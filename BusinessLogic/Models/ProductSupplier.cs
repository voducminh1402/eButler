using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BusinessLogic.Models
{
    [Table("ProductSupplier")]
    [Index(nameof(ProductId), Name = "IX_ProductSupplier", IsUnique = true)]
    [Index(nameof(SupplierId), Name = "IX_ProductSupplier_1", IsUnique = true)]
    public partial class ProductSupplier
    {
        public ProductSupplier()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        [StringLength(50)]
        public string Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int Quantiy { get; set; }
        public double Price { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductId { get; set; }
        [Required]
        [StringLength(50)]
        public string SupplierId { get; set; }
        public double? Discount { get; set; }
        [Required]
        [StringLength(100)]
        public string Image { get; set; }

        [ForeignKey(nameof(ProductId))]
        [InverseProperty("ProductSupplier")]
        public virtual Product Product { get; set; }
        [ForeignKey(nameof(SupplierId))]
        [InverseProperty("ProductSupplier")]
        public virtual Supplier Supplier { get; set; }
        [InverseProperty(nameof(OrderDetail.ProductSupplier))]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
