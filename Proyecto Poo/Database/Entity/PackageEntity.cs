namespace Proyecto_Poo.Database.Entity
{
    public class PackageEntity
    {
        public Guid IdPackage { get; set; }
        public string Sender { get; set; }
        public string ShippingLocation { get; set; }
        public string Addressee { get; set; }
        public string ReceptionPlace { get; set; }

        public double Distance { get; set; }

        public bool PackageType { get; set; }


    }
}
