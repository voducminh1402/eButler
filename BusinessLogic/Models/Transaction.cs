using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BusinessLogic.Models
{
    [Keyless]
    [Table("Transaction")]
    public partial class Transaction
    {
        [StringLength(50)]
        public string Id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UpdateDate { get; set; }
        [Required]
        [StringLength(50)]
        public string PaymentId { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        [Required]
        [StringLength(50)]
        public string Status { get; set; }
        public double Amount { get; set; }
        [Required]
        [StringLength(50)]
        public string WalletId { get; set; }

        [ForeignKey(nameof(PaymentId))]
        public virtual Payment Payment { get; set; }
        [ForeignKey(nameof(WalletId))]
        public virtual Wallet Wallet { get; set; }
    }
}
