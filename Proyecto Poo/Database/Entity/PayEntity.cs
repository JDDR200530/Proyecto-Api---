namespace Proyecto_Poo.Database.Entity
{
    public class PayEntity
    {
        public Guid Id { get; set; }

        public Guid IdPackage { get; set; }

        public double Amount {  get; set; }

        public string PaymentDate { get; set; }

        public bool PayMethod { get; set; }
    }
}
