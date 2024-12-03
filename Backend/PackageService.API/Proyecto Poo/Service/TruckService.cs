using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Proyecto_Poo.Database.Contex;
using Proyecto_Poo.Dtos.Common;
using Proyecto_Poo.Dtos.Order;
using Proyecto_Poo.Dtos.Truck;
using Proyecto_Poo.Service.Interface;
using System.Transactions;

namespace Proyecto_Poo.Service
{
    public class TruckService : ITruckService
    {
        private readonly PackageServiceDbContext context;
        private readonly ILogger<TruckService> logger;
        private readonly IMapper mapper;

        public TruckService(PackageServiceDbContext context, ILogger<TruckService> logger, IMapper mapper)
        {
            this.context = context;
            this.logger = logger;
            this.mapper = mapper;
        }
        public async Task<ResponseDto<List<TruckDto>>> GetTruckListAsync()
        {
            try
            {
                // Obtener la lista de camiones
                var truckEntities = await context.Trucks.ToListAsync();

                // Mapear las entidades a DTOs
                var truckDtos = mapper.Map<List<TruckDto>>(truckEntities);

                return new ResponseDto<List<TruckDto>>
                {
                    StatusCode = 200,
                    Status = true,
                    Message = "Lista de Camiones",
                    Data = truckDtos
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener la lista de camiones");
                return new ResponseDto<List<TruckDto>>
                {
                    StatusCode = 500,
                    Status = false,
                    Message = "Se produjo un error al obtener la lista de camiones"
                };
            }
        }



        public async Task<ResponseDto<TruckDto>> GetByIdAsync(Guid id)
        {
            try
            {
                // Cargar el camión con sus órdenes asociadas
                var truckEntity = await context.Trucks
                    .Include(t => t.Orders)  
                    .ThenInclude(o => o.Packages)
                    .FirstOrDefaultAsync(t => t.TruckId == id);

                if (truckEntity == null)
                {
                    return new ResponseDto<TruckDto>
                    {
                        StatusCode = 404,
                        Status = false,
                        Message = $"El camión con ID {id} no se encontró"
                    };
                }

                // Mapear el camión y sus órdenes a TruckDto
                var truckDto = mapper.Map<TruckDto>(truckEntity);

                double totalPackageWeigth = truckEntity.Orders
                    .SelectMany(o => o.Packages)
                    .Sum(p => p.PackageWeight);

                return new ResponseDto<TruckDto>
                {
                    StatusCode = 200,
                    Status = true,
                    Message = "Camión encontrado correctamente",
                    Data = truckDto
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener el camión por ID");
                return new ResponseDto<TruckDto>
                {
                    StatusCode = 500,
                    Status = false,
                    Message = "Se produjo un error al obtener el camión"
                };
            }
        }


    }

}
