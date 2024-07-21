using Proyecto_Poo.Dtos.Clientes;
using Proyecto_Poo.Dtos.Common;

namespace Proyecto_Poo.Service.Interface
{
    public interface IClientesService
    {
        Task<ResponseDto<List<ClienteDto>>> GetClientesListAsync();

        Task<ResponseDto<ClienteDto>> GetClientesByIdAsync(Guid id);

        Task<ResponseDto<ClienteDto>> CreateAsync(ClientesCreateDto dto);

        Task<ResponseDto<ClienteDto>> EditAsync(ClientesEditDto dto, Guid id);

        Task<ResponseDto<ClienteDto>> DeleteAsync(Guid id);
    }
}
