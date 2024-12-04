using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Poo.Constanst;
using Proyecto_Poo.Dtos.Common;
using Proyecto_Poo.Dtos.Order;
using Proyecto_Poo.Dtos.Truck;
using Proyecto_Poo.Service;
using Proyecto_Poo.Service.Interface;

namespace Proyecto_Poo.Controllers
{
    [ApiController]
    [Route("/api/trucks")]
    [Authorize(AuthenticationSchemes ="Bearer")]
    public class TrucksController : ControllerBase
    {
        private readonly ITruckService truckService;

        public TrucksController(ITruckService truckService )
        {
            this.truckService = truckService;
        }

        [HttpGet]
        [Authorize(Roles = $"{RolesConstant.ADMIN}")]
        public async Task<ActionResult<Response<TruckDto>>> GetAll()
        {
            var response = await truckService.GetTruckListAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Response<TruckDto>>> GetOneById(Guid id)
        {
            var response = await truckService.GetByIdAsync(id);
            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data,
            });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<TruckDto>>> Create(TruckCreateDto dto) 
        {
            var response = await truckService.CreateAsync(dto);
            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data,
            });
        }
    }
}
