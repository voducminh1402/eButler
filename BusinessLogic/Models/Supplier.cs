using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessLogic.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            ProductSuppliers = new HashSet<ProductSupplier>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Disciption { get; set; }
        public string Image { get; set; }

        public virtual User IdNavigation { get; set; }
        public virtual ICollection<ProductSupplier> ProductSuppliers { get; set; }
    }
}
