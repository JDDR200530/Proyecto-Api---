using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Poo.Database.Entity
{
    [Table("customers", Schema = "dbo")]
    public class CustomerEntity
    {
        [Key]
        [Column("customer_id")]
        public Guid CustomerId { get; set; }

        [Column("customer_name")]
        public string CustomerName { get; set; }

        [Column("customer_identity")]
        public long CustomerIdentity {  get; set; }

        [Column("customer_address")]
        public string CustomerAddress { get; set; }

        public virtual UserEntity CreatedByUser { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }
    }
}
