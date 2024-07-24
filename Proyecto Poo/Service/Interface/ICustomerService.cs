using Proyecto_Poo.Dtos.Clientes;
using Proyecto_Poo.Dtos.Common;

namespace Proyecto_Poo.Service.Interface
{
    public interface ICustomerService
    {
        Task<ResponseDto<List<CustomerDto>>> GetClientesListAsync();

        Task<ResponseDto<CustomerDto>> GetClientesByIdAsync(Guid id);

        Task<ResponseDto<CustomerDto>> CreateAsync(CustomerCreateDto dto);

        Task<ResponseDto<CustomerDto>> EditAsync(CustomerEditDto dto, Guid id);

        Task<ResponseDto<CustomerDto>> DeleteAsync(Guid id);
    }
}
