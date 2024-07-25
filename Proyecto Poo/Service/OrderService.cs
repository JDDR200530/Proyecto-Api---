using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Proyecto_Poo.Database.Contex;
using Proyecto_Poo.Database.Entity;
using Proyecto_Poo.Dtos.Common;
using Proyecto_Poo.Dtos.Order;
using Proyecto_Poo.Service.Interface;

namespace Proyecto_Poo.Service
{
    public class OrderService : IOrderService
    {
        private readonly PackageServiceDbContext _contex;
        private readonly IAuthService _authService;
        private readonly ILogger<OrderService> _logger;
        private readonly IMapper _mapper;

        public OrderService(PackageServiceDbContext contex, IAuthService authService, ILogger<OrderService> logger, IMapper mapper)
        {
            this._contex = contex;
            this._authService = authService;
            this._logger = logger;
            this._mapper = mapper;
        }

        public async Task<ResponseDto<OrderDto>> GetByIdAsync(Guid id)
        {
            var orderEntity = await _contex.Orders.Include(x => x.OrderId).FirstOrDefaultAsync(x => x.OrderId == id);
            if (orderEntity == null)
            {
                return new ResponseDto<OrderDto>()
                {
                    StatusCode = 404,
                    Status = false,
                    Message = $"El registro {id} no fue encontrado"
                };
            }
            var orderDto = _mapper.Map<OrderDto>(orderEntity);
            return new ResponseDto<OrderDto>()
            {
                StatusCode = 200,
                Status = true,
                Message = "Resgistrado Correctamente",
                Data = orderDto,
            };
        }
        public async Task<ResponseDto<OrderDto>> CreateAsync(OrderCreateDto dto)
        {
            using (var transaction = await _contex.Database.BeginTransactionAsync())
            {
                try
                {
                    var orderEntity = _mapper.Map<OrderEntity>(dto);
                    orderEntity.Address = dto.Address;
                        }
                catch (Exception e)
                {
                }
        }
        }
    } }

