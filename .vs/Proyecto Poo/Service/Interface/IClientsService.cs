using Proyecto_Poo.Dtos.Clientes;
using Proyecto_Poo.Dtos.Common;

namespace Proyecto_Poo.Service.Interface
{
    public interface IClientsService
    {
        Task<ResponseDto<List<ClientDto>>> GetClientsListAsync();

        Task<ResponseDto<ClientDto>> GetClientsByIdAsync(Guid id);

        Task<ResponseDto<ClientDto>> CreateAsync(ClientCreateDto dto);

        Task<ResponseDto<ClientDto>> EditAsync(ClientEditDto dto, Guid id);

        Task<ResponseDto<ClientDto>> DeleteAsync(Guid id);
    }
}
