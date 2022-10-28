using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessLogic.Models
{
    public partial class Transaction
    {
        public string Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string PaymentType { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public double Amount { get; set; }
        public string WalletId { get; set; }
        public string OrderId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Wallet Wallet { get; set; }
    }
}
