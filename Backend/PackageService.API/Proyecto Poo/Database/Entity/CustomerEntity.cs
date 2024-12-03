using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Poo.Database.Entity
{
    [Table("customers", Schema = "dbo")]
    public class CustomerEntity : BaseEntity
    {

        [Required]
        [MaxLength(500)]
        [Column("customer_name")]
        public string CustomerName { get; set; }

        [Required]
        [MaxLength(13)]
        [Column("customer_identity")]
        public long CustomerIdentity { get; set; }
        [Required]
        [Column("customer_address")]
        [MaxLength(500)]
        public string CustomerAddress { get; set; }

        public virtual UserEntity CreatedByUser { get; set; }
        public virtual UserEntity UpdatedByUser { get; set; }
    }
}
