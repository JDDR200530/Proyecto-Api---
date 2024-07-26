using AutoMapper;
using Proyecto_Poo.Database.Entity;
using Proyecto_Poo.Dtos.Order;
using Proyecto_Poo.Dtos.Package;

namespace Proyecto_Poo.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            MapsForOrders();
            MapsForPackages();
            
        }


        private void MapsForOrders()
        {
            CreateMap<OrderEntity, OrderDto>(); // mapea los valores que coinciden entre donde se envia a donde se recibe
            CreateMap<OrderCreateDto, OrderEntity>(); 
            CreateMap<OrderEditDto, OrderEntity>();
        }
        private void MapsForPackages()
        {
            CreateMap<PackageEntity, PackageDto>(); 
            CreateMap<PackageCreateDto, PackageEntity>();
            CreateMap<PackageEditDto, PackageEntity>();
        }

    }

    
}

