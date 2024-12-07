namespace Proyecto_Poo.Dtos.Shipments
{
    public class ShipmentsCreateDto
    {
        public Guid OrderId { get; set; }
        public Guid PaymentId { get; set; }
    }
}
