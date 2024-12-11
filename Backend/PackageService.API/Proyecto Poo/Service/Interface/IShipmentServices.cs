using Proyecto_Poo.Dtos.Common;
using Proyecto_Poo.Dtos.Shipments;

namespace Proyecto_Poo.Service.Interface
{
    public interface IShipmentServices
    {
        Task<ResponseDto<ShipmentDto>> CreateShipmentAsync(ShipmentsCreateDto dto);
        Task<ResponseDto<bool>> DeleteShipmentAsync(Guid shipmentId);
        Task<ResponseDto<List<ShipmentDto>>> GetAllShipmentsAsync();
        Task<ResponseDto<List<ShipmentDto>>> GetAllShipmentsByUserAsync(Guid createdById);
    }
}
