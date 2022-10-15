using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BusinessLogic.Models
{
    [Table("Wallet")]
    public partial class Wallet
    {
        [Key]
        [StringLength(50)]
        public string Id { get; set; }
        public double Amount { get; set; }
        [Required]
        [StringLength(50)]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("Wallets")]
        public virtual User User { get; set; }
    }
}
