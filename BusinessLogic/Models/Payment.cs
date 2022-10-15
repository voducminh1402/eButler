using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BusinessLogic.Models
{
    [Table("Payment")]
    public partial class Payment
    {
        [Key]
        [StringLength(50)]
        public string Id { get; set; }
        [Required]
        [StringLength(50)]
        public string OrderId { get; set; }
        public double Price { get; set; }
        [Required]
        [StringLength(50)]
        public string Status { get; set; }
        [Required]
        [StringLength(50)]
        public string PaymentType { get; set; }
        [StringLength(200)]
        public string FailReason { get; set; }

        [ForeignKey(nameof(OrderId))]
        [InverseProperty("Payments")]
        public virtual Order Order { get; set; }
    }
}
