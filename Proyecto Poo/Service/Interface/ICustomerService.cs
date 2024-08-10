using Proyecto_Poo.Dtos.Clientes;
using Proyecto_Poo.Dtos.Common;

namespace Proyecto_Poo.Service.Interface
{
    public interface ICustomerService
    {
        Task<ResponseDto<CustomerDto>> CreateAsync(CustomerCreateDto dto);
        Task<ResponseDto<CustomerDto>> DeleteAsync(Guid id);
        Task<ResponseDto<CustomerDto>> EditAsync(CustomerEditDto dto, Guid id);
        Task<ResponseDto<List<CustomerDto>>> GetCustomerListAsync();
        Task<ResponseDto<CustomerDto>> GetCustomersByIdAsync(Guid id);
    }
}
