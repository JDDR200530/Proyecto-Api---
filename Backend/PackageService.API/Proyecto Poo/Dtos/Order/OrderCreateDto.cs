﻿using System.ComponentModel.DataAnnotations;

namespace Proyecto_Poo.Dtos.Order
{
    public class OrderCreateDto
    {
        //public Guid OrderId { get; set; }

        //public DateTime OrderDate { get; set; }
        [Display(Name = "Nombre del remitente")]
        [Required(ErrorMessage ="El {0} es requerido")]
        public string SenderName { get; set; }

        [Display(Name= "Direccion del Paquete" )]
        [Required(ErrorMessage ="La {0} es requerida")]
        public string Address { get; set; }

        [Display(Name = "Nombre del receptor")]
        [Required(ErrorMessage ="El {0} es requerido")]
        public string ReceiverName { get; set; }

        public DateTime OrderDate { get; set; }

        public double TotalWeigth { get; set; }
        public double Distance { get; set; }

        public bool PaymentStatus { get; set; } = false;

    }
}
