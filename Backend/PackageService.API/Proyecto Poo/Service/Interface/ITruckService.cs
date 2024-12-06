using Proyecto_Poo.Dtos.Common;
using Proyecto_Poo.Dtos.Truck;

namespace Proyecto_Poo.Service.Interface
{
    public interface ITruckService
    {
        Task<ResponseDto<TruckDto>> CreateAsync(TruckCreateDto dto);
        Task<ResponseDto<TruckDto>> DeleteTruckAsync(Guid id);
        Task<ResponseDto<TruckDto>> GetByIdAsync(Guid id);
        Task<ResponseDto<List<TruckDto>>> GetTruckListAsync();
    }
}
