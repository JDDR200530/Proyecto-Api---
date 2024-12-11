using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Proyecto_Poo.Database.Contex;
using Proyecto_Poo.Database.Entity;
using Proyecto_Poo.Dtos.Common;
using Proyecto_Poo.Dtos.Shipments;
using Proyecto_Poo.Service.Interface;
using System.Security.Cryptography;
using System.Text;

namespace Proyecto_Poo.Service
{

    public class ShipmentService : IShipmentServices
    {
        private readonly PackageServiceDbContext _context;
        private readonly IAudtiService _auditService;
        private readonly ILogger<ShipmentService> _logger;
        private readonly IMapper _mapper;

        public ShipmentService(
            PackageServiceDbContext context,
            IAudtiService auditService,
            ILogger<ShipmentService> logger,
            IMapper mapper)
        {
            _context = context;
            _auditService = auditService;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Crear un envío y retornar el CreatedBy
        /// </summary>
        public async Task<ResponseDto<ShipmentDto>> CreateShipmentAsync(ShipmentsCreateDto dto)
        {
            try
            {
                var order = await _context.Orders
                    .Include(o => o.Packages)
                    .FirstOrDefaultAsync(o => o.Id == dto.OrderId);

                if (order == null || !order.PaymentStatus)
                {
                    return new ResponseDto<ShipmentDto>
                    {
                        StatusCode = 400,
                        Status = false,
                        Message = "La orden no fue encontrada o el pago no ha sido realizado"
                    };
                }

                var payment = await _context.Payments
                    .FirstOrDefaultAsync(p => p.Id == dto.PaymentId && p.OrderId == dto.OrderId);

                if (payment == null)
                {
                    return new ResponseDto<ShipmentDto>
                    {
                        StatusCode = 400,
                        Status = false,
                        Message = "El pago no es válido o no está asociado con la orden proporcionada"
                    };
                }

                var truck = await _context.Trucks
                    .FirstOrDefaultAsync(t => t.TruckCapacity >= order.TotalWeight);

                if (truck == null)
                {
                    return new ResponseDto<ShipmentDto>
                    {
                        StatusCode = 400,
                        Status = false,
                        Message = "No se encontró un camión disponible con la capacidad requerida"
                    };
                }

                var userId = _auditService.GetUserId() ?? "System";

                var shipment = new ShipmentEntity
                {
                    Id = Guid.NewGuid(),
                    ShipmentNumber = "ENV-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                    PaymentId = payment.Id,
                    OrderId = order.Id,
                    PackageId = order.Packages.FirstOrDefault()?.Id ?? Guid.Empty,
                    TruckId = truck.Id,
                    CreatedBy = userId,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedBy = userId,
                    UpdatedDate = DateTime.UtcNow
                };

                _context.Shipments.Add(shipment);
                await _context.SaveChangesAsync();

                await UpdateTruckCapacityAsync(truck.Id);

                return new ResponseDto<ShipmentDto>
                {
                    StatusCode = 201,
                    Status = true,
                    Message = "Envío creado correctamente",
                    Data = new ShipmentDto
                    {
                        CreatedBy = shipment.CreatedBy,
                        ShipmentNumber = shipment.ShipmentNumber,
                        PaymentId = shipment.PaymentId,
                        OrderId = shipment.OrderId,
                        TruckId = shipment.TruckId,
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el envío");
                return new ResponseDto<ShipmentDto>
                {
                    StatusCode = 500,
                    Status = false,
                    Message = "Se produjo un error inesperado",
                    Data = null
                };
            }
        }

        /// <summary>
        /// Listar envíos basados en el ID de `CreatedBy`
        /// </summary>
        public async Task<ResponseDto<List<ShipmentDto>>> GetAllShipmentsByUserAsync(Guid createdById)
        {
            try
            {
                var shipments = await _context.Shipments
                    .Where(s => s.CreatedBy == createdById.ToString())
                    .Include(s => s.Order)
                    .Include(s => s.Payment)
                    .Include(s => s.Truck)
                    .ToListAsync();

                if (!shipments.Any())
                {
                    return new ResponseDto<List<ShipmentDto>>
                    {
                        StatusCode = 404,
                        Status = false,
                        Message = "No se encontraron envíos para este usuario",
                        Data = null
                    };
                }

                var shipmentDtos = _mapper.Map<List<ShipmentDto>>(shipments);

                return new ResponseDto<List<ShipmentDto>>
                {
                    StatusCode = 200,
                    Status = true,
                    Message = "Lista de envíos obtenida correctamente",
                    Data = shipmentDtos
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de envíos");
                return new ResponseDto<List<ShipmentDto>>
                {
                    StatusCode = 500,
                    Status = false,
                    Message = "Se produjo un error inesperado",
                    Data = null
                };
            }
        }

        /// <summary>
        /// Actualizar la capacidad de un camión después de crear o eliminar un envío
        /// </summary>
        public async Task UpdateTruckCapacityAsync(Guid truckId)
        {
            try
            {
                var truck = await _context.Trucks
                    .Include(t => t.Shipment)
                    .FirstOrDefaultAsync(t => t.Id == truckId);

                if (truck != null)
                {
                    double totalWeight = truck.Shipment.Sum(s => s.Order.TotalWeight);
                    truck.TruckCapacity = Math.Max(truck.TruckCapacity - totalWeight, 0);

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar la capacidad del camión {truckId}");
            }
        }
        public async Task<ResponseDto<bool>> DeleteShipmentAsync(Guid shipmentId)
        {
            var shipment = await _context.Shipments
                .Include(s => s.Truck)
                .FirstOrDefaultAsync(s => s.Id == shipmentId);

            if (shipment == null)
            {
                return new ResponseDto<bool>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "El envío no fue encontrado",
                    Data = false
                };
            }

            _context.Shipments.Remove(shipment);
            await _context.SaveChangesAsync();

            await UpdateTruckCapacityAsync(shipment.TruckId);

            return new ResponseDto<bool>
            {
                StatusCode = 200,
                Status = true,
                Message = "Envío eliminado correctamente",
                Data = true
            };
        }


        public async Task<ResponseDto<List<ShipmentDto>>> GetAllShipmentsAsync()
        {
            try
            {
                // Obtener todos los envíos desde la base de datos
                var shipments = await _context.Shipments
                    .Include(s => s.Order)    // Incluir la información de la orden asociada
                    .Include(s => s.Payment)  // Incluir la información del pago asociado
                    .Include(s => s.Truck)    // Incluir la información del camión asociado
                    .ToListAsync();

                // Verificar si no se encontraron envíos
                if (!shipments.Any())
                {
                    return new ResponseDto<List<ShipmentDto>>
                    {
                        StatusCode = 404,
                        Status = false,
                        Message = "No se encontraron envíos",
                        Data = null
                    };
                }

                // Mapear las entidades de Shipment a DTOs
                var shipmentDtos = _mapper.Map<List<ShipmentDto>>(shipments);

                return new ResponseDto<List<ShipmentDto>>
                {
                    StatusCode = 200,
                    Status = true,
                    Message = "Lista de envíos obtenida correctamente",
                    Data = shipmentDtos
                };
            }
            catch (Exception ex)
            {
                // Manejo de errores
                _logger.LogError(ex, "Error al obtener la lista de todos los envíos");
                return new ResponseDto<List<ShipmentDto>>
                {
                    StatusCode = 500,
                    Status = false,
                    Message = "Se produjo un error inesperado",
                    Data = null
                };
            }
        }


    }
}




