    using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessLogic.Models
{
    public partial class Shipping
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public string Distric { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Note { get; set; }
        public string HouseKeeperId { get; set; }
        public virtual HouseKeeper HouseKeeper { get; set; }
    }
}
