using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessLogic.Models
{
    public partial class HouseKeeper
    {
        public HouseKeeper()
        {
            Orders = new HashSet<Order>();
            Shippings = new HashSet<Shipping>();
        }

        public string Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Image { get; set; }

        public virtual User IdNavigation { get; set; }
        public virtual Wallet Wallet { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Shipping> Shippings { get; set; }
    }
}
