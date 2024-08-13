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
        private readonly IAuthService _authService;

        public PackageService(PackageServiceDbContext context,
            ILogger<PackageService> logger,
            IMapper mapper,
            IAuthService authService)
        {
            this._context = context;
            this._logger = logger;
            this._mapper = mapper;
            this._authService = authService;
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
                var orderEntity = await _context.Packages.FirstOrDefaultAsync(o => o.PackageId == id);

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
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {




                    var packageEntity = _mapper.Map<PackageEntity>(dto);
                    Guid orderId = new Guid("7B9B23E8-8F7A-48F5-A91D-F6E38EC67F1A");
                    // To Do Ver de Mandar El Id del cliente que haga la orden 
                    packageEntity.OrderId = orderId;

                    _context.Packages.Add(packageEntity);

                    await _context.SaveChangesAsync();


                    var packageDto = _mapper.Map<PackageDto>(packageEntity);

                    await transaction.CommitAsync();

                    return new ResponseDto<PackageDto>
                    {
                        StatusCode = 201,
                        Status = true,
                        Message = "El paquete agregado al pedido correctamnete",
                        Data = packageDto,
                    };
                }

                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(e, "Error al agregar el paquete");

                    return new ResponseDto<PackageDto>
                    {
                        StatusCode = 500,
                        Status = false,
                        Message = "Error al agregar el paquete",
                    };
                }



            }
        }

            public async Task<ResponseDto<PackageDto>> EditPackageAsync(PackageEditDto dto, Guid id)
            {
                var packageEntity = await _context.Packages.FirstOrDefaultAsync(o => o.PackageId == id);

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
            var packageEntity = await _context.Packages.FirstOrDefaultAsync(o => o.PackageId == id);

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
