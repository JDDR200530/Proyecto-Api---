using Microsoft.AspNetCore.Mvc;
using Proyecto_Poo.Database.Entity;

namespace Proyecto_Poo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : ControllerBase
    {
        private List<ClienteEntity> clientes;

    }
}
