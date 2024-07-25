using Proyecto_Poo.Dtos.Common;
using Proyecto_Poo.Dtos.Order;

namespace Proyecto_Poo.Service.Interface
{
    public interface IOrderService
    {
        Task<ResponseDto<OrderDto>> CreateAsync(OrderCreateDto dto);
        Task<ResponseDto<OrderDto>> GetByIdAsync(Guid id);
    }
}
