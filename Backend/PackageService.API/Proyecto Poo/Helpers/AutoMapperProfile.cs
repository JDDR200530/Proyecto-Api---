using AutoMapper;
using Proyecto_Poo.Database.Entity;
using Proyecto_Poo.Dtos.Clientes;
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
            MapsForCustomer();
         


        }


        private void MapsForOrders()
        {
            CreateMap<OrderEntity, Dtos.Order.OrderDto>();
            CreateMap<OrderCreateDto, OrderEntity>();
            CreateMap<OrderEditDto, OrderEntity>();
        }
        private void MapsForPackages()
        {
            CreateMap<PackageEntity, PackageDto>();
            CreateMap<PackageCreateDto, PackageEntity>();
            CreateMap<PackageEditDto, PackageEntity>();
        }

        private void MapsForCustomer()
        {
            CreateMap<CustomerEntity, Dtos.Clientes.CustomerDto>();
            CreateMap<CustomerCreateDto, CustomerEntity>();
            CreateMap<CustomerEditDto, CustomerEntity>();
        }

        
    }

    
}

