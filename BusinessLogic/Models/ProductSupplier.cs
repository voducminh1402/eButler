using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessLogic.Models
{
    public partial class ProductSupplier
    {
        public ProductSupplier()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public int Quantiy { get; set; }
        public double Price { get; set; }
        public string ProductId { get; set; }
        public string SupplierId { get; set; }
        public double? Discount { get; set; }
        public string Image { get; set; }

        public virtual Product Product { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
