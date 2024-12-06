﻿using AutoMapper;
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
            var ordersEntity = await _context.Orders.ToListAsync();

            var ordersDtos = _mapper.Map<List<OrderDto>>(ordersEntity);

            return new ResponseDto<List<OrderDto>>
            {
                StatusCode = 200,
                Status = true,
                Message = "Lista de Registros obtenida correctamnete",
                Data = ordersDtos
            };

        }



        public async Task<ResponseDto<OrderDto>> GetByIdAsync(Guid id)
        {
            try
            {
                var orderEntity = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);

                if (orderEntity == null)
                {
                    return new ResponseDto<OrderDto>
                    {
                        StatusCode = 404,
                        Status = false,
                        Message = $"El registro {id} no fue encontrado"
                    };
                }

                var orderDto = _mapper.Map<OrderDto>(orderEntity);
                return new ResponseDto<OrderDto>
                {
                    StatusCode = 200,
                    Status = true,
                    Message = "Registro encontrado correctamente",
                    Data = orderDto,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el pedido por ID");
                return new ResponseDto<OrderDto>
                {
                    StatusCode = 500,
                    Status = false,
                    Message = $"Se produjo un error al obtener el pedido"
                };
            }
        }

        public async Task<ResponseDto<OrderDto>> CreateAsync(OrderCreateDto dto)
        {
            var orderEntity = new OrderEntity
            {
                OrderDate = dto.OrderDate,
                SenderName = dto.SenderName,
                Address = dto.Address,
                ReceiverName = dto.ReceiverName,
                TotalWeight = 0

            };
            _context.Orders.Add(orderEntity);
            await _context.SaveChangesAsync();
            return new ResponseDto<OrderDto>
            {
                StatusCode = 201,
                Status = true,
                Message = "Orden Creada Correctamente",
                Data = new OrderDto
                {
                    Id = orderEntity.Id,
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
            _mapper.Map(dto, orderEntity);
            orderEntity.OrderDate = DateTime.Now;

            _context.Orders.Update(orderEntity);
            await _context.SaveChangesAsync();
            var orderDto = _mapper.Map<OrderDto>(orderEntity);
            return new ResponseDto<OrderDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "El pedido sea editado correctamente",
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

            _context.Orders.Remove(orderEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<OrderDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "El pedido se a borrado correctamente "
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
