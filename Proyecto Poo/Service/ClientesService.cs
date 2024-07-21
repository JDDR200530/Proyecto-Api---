using Proyecto_Poo.Dtos.Clientes;
using Proyecto_Poo.Dtos.Common;
using Proyecto_Poo.Service.Interface;

namespace Proyecto_Poo.Service
{
    public class ClientesService : IClientesService
    {
        public readonly string _JSON_FILE;
        public ClientesService()
        {
            _JSON_FILE = "SeedData/Clientes.json";
        }
        public async Task<ResponseDto<List<ClienteDto>>> GetClientesListAsync()
        {
            return new ResponseDto<List<ClienteDto>>
            {
                StatusCode = 200,
                Status = true,
                Message = "Listado de resgistro obtenido correctamente",
                Data = await ReadClienteFromFileAsync(),
            };
        }

        public Task<ResponseDto<ClienteDto>> GetClientesByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public Task<ResponseDto<ClienteDto>> CreateAsync(ClientesCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<ClienteDto>> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<ClienteDto>> EditAsync(ClientesEditDto dto, Guid id)
        {
            throw new NotImplementedException();
        }

       

        
    }
}