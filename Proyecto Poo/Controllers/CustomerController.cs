using Microsoft.AspNetCore.Mvc;
using Proyecto_Poo.Database.Entity;
using Proyecto_Poo.Dtos.Clientes;
using Proyecto_Poo.Dtos.Common;
using Proyecto_Poo.Service.Interface;

namespace Proyecto_Poo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private List<CustomerEntity> customers;
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            this._customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<CustomerDto>>>> GetAll()
        {
            var response = await _customerService.GetClientesListAsync();
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<List<CustomerDto>>>> Get(Guid id)
        {
            var response = await _customerService.GetClientesByIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<List<CustomerDto>>>> Create(CustomerCreateDto dto)
        {
            var response = await _customerService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        
         public async Task<ActionResult<ResponseDto<List<CustomerDto>>>> Edit(CustomerEditDto dto, Guid id)
        {
            var response = await _customerService.EditAsync(dto, id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("{id}")]

        public async Task<ActionResult> Delete(Guid id)
        {
            var response = await _customerService.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
