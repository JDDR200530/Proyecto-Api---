using Proyecto_Poo.Dtos.Common;
using Proyecto_Poo.Dtos.Order;
using Proyecto_Poo.Dtos.Package;

namespace Proyecto_Poo.Service.Interface
{
    public interface IPackageService 
    {
        
        Task<ResponseDto<PackageDto>> GetPackageByIdAsync(Guid id);
        Task<ResponseDto<List<PackageDto>>> GetPackageListAsync();
        Task<ResponseDto<PackageDto>> CreatePackageAsync(PackageCreateDto dto);
        Task<ResponseDto<PackageDto>> DeletePackageAsync(Guid id);
        Task<ResponseDto<PackageDto>> EditPackageAsync(PackageEditDto dto, Guid id);
        
        
    }
}
