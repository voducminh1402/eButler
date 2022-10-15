using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BusinessLogic.Models
{
    [Table("OrderDetail")]
    [Index(nameof(OrderId), Name = "IX_OrderDetail")]
    [Index(nameof(ProductSupplierId), Name = "IX_OrderDetail_1")]
    public partial class OrderDetail
    {
        [Key]
        [StringLength(50)]
        public string OrderId { get; set; }
        [Key]
        [StringLength(50)]
        public string ProductSupplierId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }

        [ForeignKey(nameof(OrderId))]
        [InverseProperty("OrderDetails")]
        public virtual Order Order { get; set; }
        [ForeignKey(nameof(ProductSupplierId))]
        [InverseProperty("OrderDetails")]
        public virtual ProductSupplier ProductSupplier { get; set; }
    }
}
