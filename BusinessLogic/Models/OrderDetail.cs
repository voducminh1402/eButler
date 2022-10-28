using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessLogic.Models
{
    public partial class OrderDetail
    {
        public string OrderId { get; set; }
        public string ProductSupplierId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }

        public virtual Order Order { get; set; }
        public virtual ProductSupplier ProductSupplier { get; set; }
    }
}
