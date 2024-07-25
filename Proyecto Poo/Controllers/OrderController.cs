using Azure;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Poo.Dtos.Common;
using Proyecto_Poo.Dtos.Order;
using Proyecto_Poo.Service.Interface;

namespace Proyecto_Poo.Controllers
{
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Response<OrderDto>>> GetOneById(Guid id)
        {
            var response = await _orderService.GetByIdAsync(id);
            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data,
            });
        }
        [HttpPost]
        public async Task<ActionResult<ResponseDto<OrderDto>>> Create(OrderCreateDto dto)
        {
            var response = await _orderService.CreateAsync(dto);
            return StatusCode(response.StatusCode, new
            {
                response.Status, response.Message,
            });
        }
    }
}
