using Proyecto_Poo.Dtos.Common;
using Proyecto_Poo.Dtos.Payments;

namespace Proyecto_Poo.Service.Interface
{
    public interface IPaymentService
    {
        Task<ResponseDto<PaymentDto>> CreatePaymentWithDebitCardAsync(PaymentCreateDto dto);
        Task<ResponseDto<PaymentDto>> CreatePaymentWithPayPalAsync(PaymentPayPalCreatedDto dto);
    }
}
