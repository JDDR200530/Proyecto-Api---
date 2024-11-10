﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Poo.Database.Entity
{
    public class BaseEntity
    {    
    
        [Column("id")]
        public Guid Id { get; set; }

        [StringLength(450)]
        [Column("created_by")]
        public string CreatedBy { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [StringLength(450)]
        [Column("updated_by")]
        public string UpdatedBy { get; set; }

        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; }
    }
}