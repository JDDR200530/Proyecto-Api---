using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Poo.Constanst;
using Proyecto_Poo.Dtos.Payments;
using Proyecto_Poo.Service.Interface;

namespace Proyecto_Poo.Controllers
{
    [ApiController]
    [Route("/api/pay")]
    [Authorize(AuthenticationSchemes ="Bearer")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        [HttpPost]
        [Authorize(Roles = $"{RolesConstant.USER}")]
        public async Task<ActionResult<Response<PaymentDto>>> Create(PaymentCreateDto dto) 
        {
            var response = await paymentService.CreatePaymentWithDebitCardAsync(dto);
            return StatusCode(response.StatusCode, new
            {
                response.Status, response.Message, response.Data
            });
        }

        [HttpGet("{id}")]
        [Authorize(Roles =$"{RolesConstant.USER}")]
        public async Task<ActionResult<Response<PaymentDto>>> GetOneById (Guid id)
        {
            var response = await paymentService.GetPaymentByIdAsync(id);
            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message, response.Data
            });
        }

        [HttpPost("paypal")]
        [Authorize(Roles =$"{RolesConstant.USER}")]

        public async Task<ActionResult<Response<PaymentDto>>> CreatePayPal (PaymentPayPalCreatedDto dto)
        
        {
            var response = await paymentService.CreatePaymentWithPayPalAsync(dto);
            return StatusCode(response.StatusCode, new
            {
                response.Status, response.Message, response.Data
            });
        }
 
    }
}
