using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Poo.Constanst;
using Proyecto_Poo.Dtos.Auth;
using Proyecto_Poo.Dtos.Common;
using Proyecto_Poo.Dtos.Shipments;
using Proyecto_Poo.Service;
using Proyecto_Poo.Service.Interface;

namespace Proyecto_Poo.Controllers
{
    [ApiController]
    [Route("/api/shipments")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentServices shipmentServices;

        public ShipmentController(IShipmentServices shipmentServices) 
        {
            this.shipmentServices = shipmentServices;
        }


        [HttpPost]
        [Authorize(Roles = $"{RolesConstant.USER}")]

        public async Task<ActionResult<Response<ShipmentDto>>> Create (ShipmentsCreateDto dto)
        {
            var response = await shipmentServices.CreateShipmentAsync (dto);
            return StatusCode(response.StatusCode, new
            {
                response.Status, response.Message, response.Data
            });
        }

        [HttpPost("list")]
        public async Task<ActionResult<Response<ShipmentDto>>> GetOneById([FromBody] Guid id)
        {
            // Asegúrate de que shipmentServices esté definido y que GetAllShipmentsByUserAsync sea un método válido.
            var response = await shipmentServices.GetAllShipmentsByUserAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Authorize(Roles = $"{RolesConstant.ADMIN}")]

        public async Task<ActionResult<Response<ShipmentDto>>> GetAll()
        {
            var response = await shipmentServices.GetAllShipmentsAsync();
            return StatusCode(response.StatusCode, response);
        }

    }
}
