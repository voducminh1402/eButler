using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessLogic.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductSuppliers = new HashSet<ProductSupplier>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<ProductSupplier> ProductSuppliers { get; set; }
    }
}
