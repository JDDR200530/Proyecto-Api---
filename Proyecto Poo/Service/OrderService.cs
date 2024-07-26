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

        public async Task<ResponseDto<OrderDto>> GetByIdAsync(Guid id)
        {
            try
            {
                var orderEntity = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == id);
                
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
            var orderEntity = _mapper.Map<OrderEntity>(dto);
            orderEntity.OrderId = new Guid();
            orderEntity.OrderDate = DateTime.Now;
            
            _context.Orders.Add(orderEntity);
            // Guardar cambios
            await _context.SaveChangesAsync();

            // mapeamos cuando posee el category entity el id para poder obtenerlo una vez creado
            // Pues al crear no existe debe generarse
            var orderDto = _mapper.Map<OrderDto>(orderEntity);

            return new ResponseDto<OrderDto>
            {
                StatusCode = 201,
                Status = true,
                Message = "El pedido sea  creado correctamnete",
                Data = orderDto,
            };
        }
        public async Task<ResponseDto<OrderDto>> EditAsync(OrderEditDto dto, Guid id) 
        {
            var orderEntity = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == id);

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
            var orderEntity = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == id);

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
    }
}
