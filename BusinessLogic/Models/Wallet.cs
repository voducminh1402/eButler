using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessLogic.Models
{
    public partial class Wallet
    {
        public Wallet()
        {
            Transactions = new HashSet<Transaction>();
        }

        public string Id { get; set; }
        public double Amount { get; set; }

        public virtual HouseKeeper IdNavigation { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
