using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Proyecto_Poo.Database.Contex;
using Proyecto_Poo.Database.Entity;
using Proyecto_Poo.Dtos.Common;
using Proyecto_Poo.Dtos.Order;
using Proyecto_Poo.Service.Interface;
using System;
using System.Threading.Tasks;

namespace Proyecto_Poo.Service
{
    public class OrderService : IOrderService
    {
        private readonly PackageServiceDbContext _context;
        private readonly ILogger<OrderService> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public OrderService(PackageServiceDbContext context, ILogger<OrderService> logger, IMapper mapper)
        {
            this._context = context;
            this._logger = logger;
            this._mapper = mapper;
        }

        public async Task<ResponseDto<List<OrderDto>>> GetOrderListAsync()
        {
            var ordersDtos = await _context.Orders
                .Include(o => o.Packages) // Incluye los paquetes para calcular el peso
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    SenderName = o.SenderName,
                    Address = o.Address,
                    ReceiverName = o.ReceiverName,
                    Distance = o.Distance,
                    TotalWeigth = o.Packages.Sum(p => p.PackageWeight)
                    ,PaymentStatus = o.PaymentStatus,
                })
                .ToListAsync();

            return new ResponseDto<List<OrderDto>>
            {
                StatusCode = 200,
                Status = true,
                Message = "Lista de registros obtenida correctamente",
                Data = ordersDtos
            };
        }




        public async Task<ResponseDto<OrderDto>> GetByIdAsync(Guid id)
        {
            try
            {
                var order = await _context.Orders
                    .Where(o => o.Id == id)
                    .Select(o => new OrderDto
                    {
                        Id = o.Id,
                        OrderDate = o.OrderDate,
                        SenderName = o.SenderName,
                        Address = o.Address,
                        ReceiverName = o.ReceiverName,
                        Distance = o.Distance,
                        TotalWeigth = o.Packages.Sum(p => p.PackageWeight), // Suma directa desde la base de datos
                        PaymentStatus = o.PaymentStatus,
                    })
                    .FirstOrDefaultAsync();

                if (order == null)
                {
                    return new ResponseDto<OrderDto>
                    {
                        StatusCode = 404,
                        Status = false,
                        Message = $"El registro {id} no fue encontrado"
                    };
                }

                return new ResponseDto<OrderDto>
                {
                    StatusCode = 200,
                    Status = true,
                    Message = "Registro encontrado correctamente",
                    Data = order
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el pedido por ID");
                return new ResponseDto<OrderDto>
                {
                    StatusCode = 500,
                    Status = false,
                    Message = "Se produjo un error al obtener el pedido"
                };
            }
        }




        public async Task<ResponseDto<OrderDto>> CreateAsync(OrderCreateDto dto)
        {
            // Validar que la distancia sea mayor a 0
            if (dto.Distance <= 0)
            {
                return new ResponseDto<OrderDto>
                {
                    StatusCode = 400,
                    Status = false,
                    Message = "La distancia debe ser mayor a cero"
                };
            }

            // Validar explícitamente que PaymentStatus y TotalWeight sean correctos
            if (dto.PaymentStatus != false || dto.TotalWeigth != 0)
            {
                return new ResponseDto<OrderDto>
                {
                    StatusCode = 400,
                    Status = false,
                    Message = "El estado de pago debe ser falso y el peso total debe ser cero al crear una orden"
                };
            }

            // Crear la entidad con valores válidos
            var orderEntity = new OrderEntity
            {
                OrderDate = dto.OrderDate,
                SenderName = dto.SenderName,
                Address = dto.Address,
                ReceiverName = dto.ReceiverName,
                TotalWeight = 0,
                Distance = dto.Distance,
                PaymentStatus = false
            };

            _context.Orders.Add(orderEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<OrderDto>
            {
                StatusCode = 201,
                Status = true,
                Message = "Orden creada correctamente",
                Data = new OrderDto
                {
                    Id = orderEntity.Id
                }
            };

        }
        public async Task<ResponseDto<OrderDto>> EditAsync(OrderEditDto dto, Guid id)
        {
            var orderEntity = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);

            if (orderEntity == null)
            {
                return new ResponseDto<OrderDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = $"El pedido {id} no fue encontrado"
                };
            }

            if (orderEntity.PaymentStatus)
            {
                return new ResponseDto<OrderDto>
                {
                    StatusCode = 400,
                    Status = false,
                    Message = "No se puede editar el pedido porque el estado de pago es verdadero"
                };
            }

            _mapper.Map(dto, orderEntity);
            orderEntity.OrderDate = DateTime.Now;

            _context.Orders.Update(orderEntity);
            await _context.SaveChangesAsync();
            var orderDto = _mapper.Map<OrderDto>(orderEntity);

            return new ResponseDto<OrderDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "El pedido se ha editado correctamente",
                Data = orderDto,
            };
        }


        public async Task<ResponseDto<OrderDto>> DeleteAsync(Guid id)
        {
            var orderEntity = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);

            if (orderEntity == null)
            {
                return new ResponseDto<OrderDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = $"El pedido {id} no fue encontrado"
                };
            }

            if (orderEntity.PaymentStatus)
            {
                return new ResponseDto<OrderDto>
                {
                    StatusCode = 400,
                    Status = false,
                    Message = "No se puede borrar el pedido porque el estado de pago es verdadero"
                };
            }

            _context.Orders.Remove(orderEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<OrderDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "El pedido se ha borrado correctamente"
            };
        }


        // Prueba para agragar el peso de la orden y eliminarloo de manera automatica 


        public async Task<ResponseDto<OrderDto>> AddPackagestoOrdersAsync(Guid orderId, List<PackageEntity> packages)
        {
            var orderEntity = await _context.Orders
            .Include(o => o.Packages)
            .FirstOrDefaultAsync(o => o.Id == orderId);

            if (orderEntity == null)
            {
                return new ResponseDto<OrderDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "Orden no encontrada "
                };
            }

            foreach (var package in packages)
            {
                var PackageEntity = new PackageEntity
                {
                    OrderId = orderId,
                    PackageWeight = package.PackageWeight
                };

                _context.Packages.Add(PackageEntity);
                orderEntity.Packages.Add(PackageEntity);
            }
            orderEntity.TotalWeight = orderEntity.Packages.Sum(p => p.PackageWeight);

            await _context.SaveChangesAsync();

            return new ResponseDto<OrderDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Paquetes agregados y peso total actualizado",
                Data = new OrderDto
                {
                    Id = orderEntity.Id,
                    TotalWeigth = orderEntity.TotalWeight
                }
            };
        }

        public async Task<ResponseDto<OrderDto>> RemovePackageAsync(Guid packageId)
        {
            var packageEntity = await _context.Packages
            .Include(p => p.Order)
            .FirstOrDefaultAsync(p => p.Id == packageId);

            if (packageEntity == null)
            {
                return new ResponseDto<OrderDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "Paquete no encontrado"
                };
            }

            var orderEntity = packageEntity.Order;
            _context.Packages.Remove(packageEntity);

            orderEntity.TotalWeight = orderEntity.Packages.Where(p => p.Id != packageId).Sum(p => p.PackageWeight);
            await _context.SaveChangesAsync();
            return new ResponseDto<OrderDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Pauqe Asido removido y el peso a sido actualizado",
                Data = new OrderDto
                {
                    Id = orderEntity.Id,
                    TotalWeigth = orderEntity.TotalWeight
                }
            };
        }
    }

}
