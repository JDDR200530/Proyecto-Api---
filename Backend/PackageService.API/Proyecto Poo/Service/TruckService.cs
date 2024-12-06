using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Proyecto_Poo.Database.Contex;
using Proyecto_Poo.Database.Entity;
using Proyecto_Poo.Dtos.Common;
using Proyecto_Poo.Dtos.Order;
using Proyecto_Poo.Dtos.Truck;
using Proyecto_Poo.Service.Interface;
using System.Diagnostics;
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
                // Seleccionar directamente las propiedades necesarias
                var truckDtos = await context.Trucks
                    .Select(truck => new TruckDto
                    {
                        Id = truck.Id,
                        TruckCapacity = truck.TruckCapacity,
                        TruckAvailable = truck.TruckCapacity > 0 // Estado basado en la capacidad
                    })
                    .ToListAsync();

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
                // Manejo de errores
                return new ResponseDto<List<TruckDto>>
                {
                    StatusCode = 500,
                    Status = false,
                    Message = $"Error al obtener la lista de camiones: {ex.Message}",
                    Data = null
                };
            }
        }




        public async Task<ResponseDto<TruckDto>> GetByIdAsync(Guid id)
        {
            try
            {
                // Cargar el camión y las órdenes asociadas
                var truckEntity = await context.Trucks
                    .Where(t => t.Id == id)
                    .Select(truck => new TruckDto
                    {
                        Id = truck.Id,
                        TruckCapacity = truck.TruckCapacity,
                        TruckAvailable = truck.TruckCapacity > 0,
                        OrderIds = truck.Shipment.Select(s => s.OrderId).ToList() // Obtener IDs de las órdenes
                    })
                    .FirstOrDefaultAsync();

                if (truckEntity == null)
                {
                    return new ResponseDto<TruckDto>
                    {
                        StatusCode = 404,
                        Status = false,
                        Message = $"El camión con ID {id} no se encontró"
                    };
                }

                return new ResponseDto<TruckDto>
                {
                    StatusCode = 200,
                    Status = true,
                    Message = "Camión encontrado correctamente",
                    Data = truckEntity
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

        public async Task<ResponseDto<TruckDto>> CreateAsync(TruckCreateDto dto)
        {
            // Mapea el DTO de entrada a una entidad
            var truckEntity = mapper.Map<TruckEntity>(dto);

            // Agrega la entidad al contexto
            context.Trucks.Add(truckEntity);

            // Guarda los cambios en la base de datos
            await context.SaveChangesAsync();

            // Mapea la entidad creada de vuelta a un DTO
            var truckDto = mapper.Map<TruckDto>(truckEntity);

            // Retorna la respuesta
            return new ResponseDto<TruckDto>
            {
                StatusCode = 201,
                Status = true,
                Message = "Se ha Creado Correctamente",
                Data = truckDto
            };
        }

        public async Task<ResponseDto<TruckDto>> DeleteTruckAsync(Guid id)
        {
            // Busca el camión en la base de datos
            var truckEntity = await context.Trucks
                .Include(t => t.Shipment) // Asegúrate de incluir las órdenes relacionadas
                .FirstOrDefaultAsync(o => o.Id == id);

            // Si el camión no existe, devuelve un error
            if (truckEntity == null)
            {
                return new ResponseDto<TruckDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = $"El camión con ID {id} no fue encontrado"
                };
            }

            // Verifica si el camión tiene órdenes asociadas
            if (truckEntity.Shipment != null && truckEntity.Shipment.Any())
            {
                return new ResponseDto<TruckDto>
                {
                    StatusCode = 400,
                    Status = false,
                    Message = $"El camión con ID {id} no se puede eliminar porque tiene órdenes asociadas"
                };
            }

            // Elimina el camión si no tiene órdenes asociadas
            context.Trucks.Remove(truckEntity);
            await context.SaveChangesAsync();

            return new ResponseDto<TruckDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "El camión se ha eliminado correctamente"
            };
        }


    }

}
