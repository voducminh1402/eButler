using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BusinessLogic.Models
{
    [Table("Supplier")]
    public partial class Supplier
    {
        [Key]
        [StringLength(50)]
        public string Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string Country { get; set; }
        [Required]
        [StringLength(200)]
        public string Address { get; set; }
        [Required]
        [StringLength(10)]
        public string Phone { get; set; }
        [Column(TypeName = "text")]
        public string Disciption { get; set; }
        [Required]
        [StringLength(100)]
        public string Image { get; set; }
        [Required]
        [StringLength(50)]
        public string CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("Suppliers")]
        public virtual Category Category { get; set; }
        [InverseProperty("Supplier")]
        public virtual ProductSupplier ProductSupplier { get; set; }
    }
}
