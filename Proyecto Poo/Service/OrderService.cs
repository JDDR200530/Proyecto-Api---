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
        private readonly PackageServiceDbContext _contex;
        private readonly ILogger<OrderService> _logger;
        private readonly IMapper _mapper;

        public OrderService(PackageServiceDbContext context, ILogger<OrderService> logger, IMapper mapper)
        {
            _contex = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ResponseDto<OrderDto>> GetByIdAsync(Guid id)
        {
            try
            {
                var orderEntity = await _contex.Orders
                    .Include(x => x.Order)  
                    .FirstOrDefaultAsync(x => x.OrderId == id);

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
                    Message = "Se produjo un error al obtener el pedido"
                };
            }
        }

        public async Task<ResponseDto<OrderDto>> CreateAsync(OrderCreateDto dto)
        {
            using (var transaction = await _contex.Database.BeginTransactionAsync())
            {
                try
                {
                    var orderEntity = _mapper.Map<OrderEntity>(dto);

                    _contex.Orders.Add(orderEntity);  // Agregar la entidad al contexto

                    await _contex.SaveChangesAsync();
                    await transaction.CommitAsync();

                    var orderDto = _mapper.Map<OrderDto>(orderEntity);
                    return new ResponseDto<OrderDto>
                    {
                        StatusCode = 201,
                        Status = true,
                        Message = "Registro creado correctamente",
                        Data = orderDto
                    };
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Error al crear el pedido");
                    return new ResponseDto<OrderDto>
                    {
                        StatusCode = 500,
                        Status = false,
                        Message = "Se produjo un error al crear el pedido"
                    };
                }
            }
        }
    }
}
