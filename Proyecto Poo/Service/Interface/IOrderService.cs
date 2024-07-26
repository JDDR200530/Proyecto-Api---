using Proyecto_Poo.Dtos.Common;
using Proyecto_Poo.Dtos.Order;

namespace Proyecto_Poo.Service.Interface
{
    public interface IOrderService
    {
        Task<ResponseDto<OrderDto>> CreateAsync(OrderCreateDto dto);
        Task<ResponseDto<OrderDto>> DeleteAsync(Guid id);
        Task<ResponseDto<OrderDto>> EditAsync(OrderEditDto dto, Guid id);
        Task<ResponseDto<OrderDto>> GetByIdAsync(Guid id);
    }
}
