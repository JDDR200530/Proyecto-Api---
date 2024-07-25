using Newtonsoft.Json;
using Proyecto_Poo.Database.Entity;
using Proyecto_Poo.Dtos.Clientes;
using Proyecto_Poo.Dtos.Common;
using Proyecto_Poo.Service.Interface;

namespace Proyecto_Poo.Service
{
    public class ClientsService : IClientsService
    {
        public readonly string _JSON_FILE;
        public ClientsService()
        {
            _JSON_FILE = "SeedData/clients.json";
        }
        public async Task<ResponseDto<List<ClientDto>>> GetClientsListAsync()
        {
            return new ResponseDto<List<ClientDto>>
            {
                StatusCode = 200,
                Status = true,
                Message = "Listado de resgistro obtenido correctamente",
                Data = await ReadClientsFromFileAsync(),
            };
        }

        public Task<ResponseDto<ClientDto>> GetClientsByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public Task<ResponseDto<ClientDto>> CreateAsync(ClientCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<ClientDto>> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<ClientDto>> EditAsync(ClientEditDto dto, Guid id)
        {
            throw new NotImplementedException();
        }

        private async Task<List<ClientDto>> ReadClientsFromFileAsync()
        {
            if (!File.Exists(_JSON_FILE))
            {
                return new List<ClientDto>();
            }

            var json = await File.ReadAllTextAsync(_JSON_FILE);

            var categories = JsonConvert.DeserializeObject<List<ClientEntity>>(json);

            var dtos = categories.Select(x => new ClientDto
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,

            }).ToList();


            return dtos;
        }

    
    }
}