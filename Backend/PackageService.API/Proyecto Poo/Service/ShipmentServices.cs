using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Proyecto_Poo.Database.Contex;
using Proyecto_Poo.Database.Entity;
using Proyecto_Poo.Dtos.Common;
using Proyecto_Poo.Dtos.Shipments;
using Proyecto_Poo.Service.Interface;

namespace Proyecto_Poo.Service
{
    public class ShipmentService : IShipmentServices
    {
        private readonly PackageServiceDbContext context;
        private readonly IAudtiService _audtiService;
        private readonly PackageServiceDbContext context1;

        public ShipmentService(PackageServiceDbContext context, IAudtiService audtiService, PackageServiceDbContext context1)
        {
            context = context;
            _audtiService = audtiService;
            this.context1 = context1;
        }

        public async Task<ResponseDto<ShipmentDto>> CreateShipmentAsync(ShipmentsCreateDto dto)
        {
            var order = await context.Orders
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

            var payment = await context.Payments
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

            var truck = await context.Trucks
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

            var userId = _audtiService.GetUserId() ?? "System";

            var shipment = new ShipmentEntity
            {
                Id = Guid.NewGuid(),
                ShipmentNumber = "ENV-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                PaymentId = payment.Id,
                OrderId = order.Id,
                PackageId = order.Packages.FirstOrDefault()?.Id ?? Guid.Empty,
                TruckId = truck.Id,
                CreatedBy = userId,
                CreatedDate = DateTime.Now,
                UpdatedBy = userId,
                UpdatedDate = DateTime.Now
            };

            context.Shipments.Add(shipment);
            await context.SaveChangesAsync();

            await UpdateTruckCapacityAsync(truck.Id); // Actualizar la capacidad del camión

            var packageIds = order.Packages.Select(p => p.Id).ToList();

            return new ResponseDto<ShipmentDto>
            {
                StatusCode = 201,
                Status = true,
                Message = "Envío creado correctamente",
                Data = new ShipmentDto
                {
                    ShipmentNumber = shipment.ShipmentNumber,
                    PaymentId = shipment.PaymentId,
                    OrderId = shipment.OrderId,
                    PackageIds = packageIds,
                    TruckId = shipment.TruckId,
                  
                }
            };
        }

        public async Task<ResponseDto<bool>> DeleteShipmentAsync(Guid shipmentId)
        {
            var shipment = await context.Shipments
                .Include(s => s.Truck)
                .FirstOrDefaultAsync(s => s.Id == shipmentId);

            if (shipment == null)
            {
                return new ResponseDto<bool>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "El envío no fue encontrado"
                };
            }

            context.Shipments.Remove(shipment);
            await context.SaveChangesAsync();

            await UpdateTruckCapacityAsync(shipment.TruckId); // Actualizar la capacidad del camión

            return new ResponseDto<bool>
            {
                StatusCode = 200,
                Status = true,
                Message = "Envío eliminado correctamente",
                Data = true
            };
        }
        public async Task UpdateTruckCapacityAsync(Guid truckId)
        {
            var truck = await context.Trucks
                .Include(t => t.Shipment)
                .FirstOrDefaultAsync(t => t.Id == truckId);

            if (truck != null)
            {
                // Calcular la capacidad utilizada
                double totalWeight = truck.Shipment.Sum(s => s.Order.TotalWeight);
                truck.TruckCapacity -= totalWeight;

                // Guardar los cambios en la base de datos
                await context.SaveChangesAsync();
            }
        }


    }


}
