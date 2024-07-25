using System;
using System.Data;

namespace Proyecto_Poo.Database.Entity
{
    public class RouteEntity
    {
        public Guid Id { get; set; }
        public string origin { get; set; }

        public string destination { get; set; }

        public double distance { get; set; }

        public DateTime time { get; set; }

        public string description { get; set; }


    }
}
