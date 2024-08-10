using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Poo.Database.Entity
{
    [Table("customers", Schema = "dbo")]
    public class CustomerEntity
    {
        [Key]
        [Column("customer_id")]
        public Guid Id { get; set; }

        [Column("customer_name")]
        public string Name { get; set; }

        [Column("customer_identity")]
        public int Identity {  get; set; }

        [Column("customer_address")]
        public string Address { get; set; }


    }
}
