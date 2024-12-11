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
            try
            {
                // Verificar si la orden existe
                var orderEntity = await _context.Orders.FirstOrDefaultAsync(o => o.Id == dto.OrderId);
                if (orderEntity == null)
                {
                    return new ResponseDto<PackageDto>
                    {
                        StatusCode = 404,
                        Status = false,
                        Message = "La orden asociada no fue encontrada."
                    };
                }

                // Validar que el payment_status de la orden no sea true
                if (orderEntity.PaymentStatus)
                {
                    return new ResponseDto<PackageDto>
                    {
                        StatusCode = 400,
                        Status = false,
                        Message = "No se pueden crear paquetes para una orden con el estado de pago completado."
                    };
                }

                // Crear la entidad del paquete
                var packageEntity = _mapper.Map<PackageEntity>(dto);
                packageEntity.Id = Guid.NewGuid(); // Generar un nuevo ID
                packageEntity.UpdatedBy = audtiService.GetUserId();

                _context.Packages.Add(packageEntity);
                await _context.SaveChangesAsync();

                // Mapear la entidad a DTO
                var packageDto = _mapper.Map<PackageDto>(packageEntity);

                return new ResponseDto<PackageDto>
                {
                    StatusCode = 201,
                    Status = true,
                    Message = "El paquete ha sido creado correctamente.",
                    Data = packageDto
                };
            }
            catch (Exception ex)
            {
                // Manejo de errores
                _logger.LogError(ex, "Error al crear el paquete.");
                return new ResponseDto<PackageDto>
                {
                    StatusCode = 500,
                    Status = false,
                    Message = "Ocurrió un error interno al crear el paquete."
                };
            }
        }


        public async Task<ResponseDto<PackageDto>> EditPackageAsync(PackageEditDto dto, Guid id)
        {
            var packageEntity = await _context.Packages
                .Include(p => p.Order) // Incluir la orden asociada
                .FirstOrDefaultAsync(p => p.Id == id);

            if (packageEntity == null)
            {
                return new ResponseDto<PackageDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = $"El paquete {id} no fue encontrado"
                };
            }

            // Validar el estado de pago de la orden asociada
            if (packageEntity.Order.PaymentStatus)
            {
                return new ResponseDto<PackageDto>
                {
                    StatusCode = 400,
                    Status = false,
                    Message = "No se puede editar el paquete porque el estado de pago de la orden es verdadero"
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
                Message = "El paquete se ha editado correctamente",
                Data = packageDto,
            };
        }




        public async Task<ResponseDto<PackageDto>> DeleteAsync(Guid id)
        {
            var packageEntity = await _context.Packages
                .Include(p => p.Order) // Incluir la orden asociada
                .FirstOrDefaultAsync(p => p.Id == id);

            if (packageEntity == null)
            {
                return new ResponseDto<PackageDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = $"El paquete con Id {id} no fue encontrado"
                };
            }

            // Validar el estado de pago de la orden asociada
            if (packageEntity.Order.PaymentStatus)
            {
                return new ResponseDto<PackageDto>
                {
                    StatusCode = 400,
                    Status = false,
                    Message = "No se puede eliminar el paquete porque el estado de pago de la orden es verdadero"
                };
            }

            _context.Packages.Remove(packageEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<PackageDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "El paquete se ha eliminado correctamente"
            };
        }






    }
}
