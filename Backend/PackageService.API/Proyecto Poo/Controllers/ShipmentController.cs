using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Poo.Constanst;
using Proyecto_Poo.Dtos.Shipments;
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
    }
}
