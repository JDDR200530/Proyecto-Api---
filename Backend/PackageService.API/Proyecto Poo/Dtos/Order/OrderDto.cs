﻿namespace Proyecto_Poo.Dtos.Order
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public DateTime OrderDate  { get; set; }

        public string SenderName { get; set; }

        public string Address { get; set; }

        public string ReceiverName { get; set; }

        public double TotalWeigth { get; set; }
    }
}
