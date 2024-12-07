namespace Proyecto_Poo.Dtos.Order
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public DateTime OrderDate  { get; set; }

        public string SenderName { get; set; }

        public string Address { get; set; }

        public string ReceiverName { get; set; }

        public double Distance { get; set; }

        public bool PaymentStatus { get; set; } = false;
        public double TotalWeigth { get; set; }
    }
}
