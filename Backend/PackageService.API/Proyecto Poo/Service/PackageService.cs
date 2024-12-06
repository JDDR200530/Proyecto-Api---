using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Proyecto_Poo.Database.Contex;
using Proyecto_Poo.Database.Entity;
using Proyecto_Poo.Dtos.Common;
using Proyecto_Poo.Dtos.Order;
using Proyecto_Poo.Dtos.Package;
using Proyecto_Poo.Service.Interface;

namespace Proyecto_Poo.Service
{
    public class PackageService : IPackageService
    {
        private readonly PackageServiceDbContext _context;
        private readonly ILogger<PackageService> _logger;
        private readonly IMapper _mapper;
        private readonly IAudtiService audtiService;

        public PackageService(PackageServiceDbContext context, ILogger<PackageService> logger, IMapper mapper, IAudtiService audtiService)
        {
            this._context = context;
            this._logger = logger;
            this._mapper = mapper;
            this.audtiService = audtiService;
        }

        public async Task<ResponseDto<List<PackageDto>>> GetPackageListAsync()
        {
            var packagesEntity = await _context.Packages.ToListAsync();

            var packagesDtos = _mapper.Map<List<PackageDto>>(packagesEntity);

            return new ResponseDto<List<PackageDto>>
            {
                StatusCode = 200,
                Status = true,
                Message = "Lista de Registros obtenida correctamnete",
                Data = packagesDtos
            };
        }
        public async Task<ResponseDto<PackageDto>> GetPackageByIdAsync(Guid id)
        {
            try
            {
                var orderEntity = await _context.Packages.FirstOrDefaultAsync(o => o.Id == id);

                if (orderEntity == null)
                {
                    return new ResponseDto<PackageDto>
                    {
                        StatusCode = 404,
                        Status = false,
                        Message = $"El registro {id} no fue encontrado"
                    };
                }

                var packageDto = _mapper.Map<PackageDto>(orderEntity);
                return new ResponseDto<PackageDto>
                {
                    StatusCode = 200,
                    Status = true,
                    Message = "Registro encontrado correctamente",
                    Data = packageDto,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el pedido por ID");
                return new ResponseDto<PackageDto>
                {
                    StatusCode = 500,
                    Status = false,
                    Message = $"Se produjo un error al obtener el pedido"
                };
            }
        }


        public async Task<ResponseDto<PackageDto>> CreatePackageAsync(PackageCreateDto dto)
        {
            var packageEntity = _mapper.Map<PackageEntity>(dto);
            packageEntity.Id = new Guid();
            packageEntity.UpdatedBy = audtiService.GetUserId();

            _context.Packages.Add(packageEntity);

            await _context.SaveChangesAsync();


            var packageDto = _mapper.Map<PackageDto>(packageEntity);

            return new ResponseDto<PackageDto>
            {
                StatusCode = 201,
                Status = true,
                Message = "El pedido sea  creado correctamnete",
                Data = packageDto,
            };
        }

        public async Task<ResponseDto<PackageDto>> EditPackageAsync(PackageEditDto dto, Guid id)
        {
            var packageEntity = await _context.Packages.FirstOrDefaultAsync(o => o.Id == id);

            if (packageEntity == null)
            {
                return new ResponseDto<PackageDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = $"El pedido {id} no fue encontrado"
                };


            }
            _mapper.Map(dto, packageEntity);
            

            _context.Packages.Update(packageEntity);
            await _context.SaveChangesAsync();
            var packageDto = _mapper.Map<PackageDto>(packageEntity);
            return new ResponseDto<PackageDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "El pedido sea editado correctamente",
                Data = packageDto,
            };
        }

    
       
        public async Task<ResponseDto<PackageDto>> DeletePackageAsync(Guid id)
        {
            var packageEntity = await _context.Packages.FirstOrDefaultAsync(o => o.Id == id);

            if (packageEntity == null)
            {
                return new ResponseDto<PackageDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = $"El pedido {id} no fue encontrado"
                };


            }

            _context.Packages.Remove(packageEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<PackageDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "El paquete se a eliminado correctamente "
            };
        }
    
    }
}
