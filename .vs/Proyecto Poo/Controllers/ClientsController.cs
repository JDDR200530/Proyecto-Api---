using Microsoft.AspNetCore.Mvc;
using Proyecto_Poo.Database.Entity;
using Proyecto_Poo.Service.Interface;

namespace Proyecto_Poo.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsService _clientsService;

        public ClientsController(IClientsService clientsService)
        {
            this._clientsService = clientsService;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _clientsService.GetClientsListAsync());
        }
    }
}
