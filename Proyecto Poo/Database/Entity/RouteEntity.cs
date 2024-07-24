using System;
using System.Data;

namespace Proyecto_Poo.Database.Entity
{
    public class RouteEntity
    {
        public Guid Id { get; set; }
        public string Origin { get; set; }

        public string Destination { get; set; }

        public double Distance { get; set; }

        public DateTime Time { get; set; }

        public string Description { get; set; }


    }
}
