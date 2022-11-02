using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessLogic.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
            Transactions = new HashSet<Transaction>();
        }

        public string Id { get; set; }
        public string UserId { get; set; }
        public double TotalPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual HouseKeeper User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
