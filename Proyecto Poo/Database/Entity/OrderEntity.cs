using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Poo.Database.Entity
{
    [Table("orders", Schema = "dbo")]
    public class OrderEntity
    {
        [Key]
        [Column("Id")]
        public Guid OrderId { get; set; }

        public DateTime CreationDate { get; set; }

        public string SenderName { get; set; }

        public string ReciverName { get; set; }

        public string Address { get; set; }

        public IEnumerable<PackageEntity> Packages { get; set; }
        public IEnumerable<PaymentEntity> Payments { get; set; }




    }
}
