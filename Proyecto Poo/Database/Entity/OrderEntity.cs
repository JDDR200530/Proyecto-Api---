using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Poo.Database.Entity
{
    [Table("orders", Schema = "dbo")]
    public class OrderEntity
    {
        [Key]
        [Column("order_id")]
        public Guid OrderId { get; set; }

        [Column("order_date")]
        public DateTime OrderDate { get; set; }
        [Column("sender_name")]
        public string SenderName { get; set; }

        public string Address { get; set; }
        [Column("reciver_name")]
        public string ReciverName { get; set; }

        public virtual IEnumerable<PackageEntity> Packages { get; set; }
    }
}
