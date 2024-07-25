using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Poo.Database.Entity
{
    [Table("packages", Schema = "dbo")]
    public class PackageEntity
    {
        [Key]
        [Column("package_id")]
        public Guid PackageId { get; set; }


        [Column("package_weight")]
        public bool PackageWeight { get; set; }

        public virtual IEnumerable<OrderPackagesEntity> Packages { get; set; }

    }
}
