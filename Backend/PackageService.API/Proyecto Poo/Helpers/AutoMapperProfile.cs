using AutoMapper;
using Proyecto_Poo.Database.Entity;
using Proyecto_Poo.Dtos.Clientes;
using Proyecto_Poo.Dtos.Order;
using Proyecto_Poo.Dtos.Package;
using Proyecto_Poo.Dtos.Payments;
using Proyecto_Poo.Dtos.Shipments;
using Proyecto_Poo.Dtos.Truck;

namespace Proyecto_Poo.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            MapsForOrders();
            MapsForPackages();
            MapsForCustomer();
            MapsForTrucks();
            MapsForPayments();
            MapsForShipments();
         


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

        private void MapsForTrucks()
        {
            CreateMap<TruckEntity, TruckDto>();
            CreateMap<TruckCreateDto, TruckEntity>();
            CreateMap<TruckEditDto, TruckEntity>();
        }

        private void MapsForPayments()
       {
            CreateMap<PaymentEntity, PaymentDto>();
            CreateMap<PaymentCreateDto, PaymentEntity>();
       }
        
        private void MapsForShipments()
        {
            CreateMap<ShipmentEntity, ShipmentDto>();
            CreateMap<ShipmentsCreateDto, ShipmentEntity>();
        }
    }

    
}

