using AutoMapper;
using Proyecto_Poo.Database.Entity;
using Proyecto_Poo.Dtos.Order;

namespace Proyecto_Poo.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            MapsForOrders();
            
        }


        private void MapsForOrders()
        {
            CreateMap<OrderEntity, OrderDto>(); // mapea los valores que coinciden entre donde se envia a donde se recibe
            
        }
    }

    
}

