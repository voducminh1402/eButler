using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BusinessLogic.Models
{
    [Table("User")]
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
            Wallets = new HashSet<Wallet>();
        }

        [Key]
        [StringLength(50)]
        public string Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        public bool IsActive { get; set; }
        [Required]
        [StringLength(50)]
        public string RoleId { get; set; }
        public bool IsSystemAdmin { get; set; }

        [ForeignKey(nameof(RoleId))]
        [InverseProperty("Users")]
        public virtual Role Role { get; set; }
        [InverseProperty("IdNavigation")]
        public virtual HouseKeeper HouseKeeper { get; set; }
        [InverseProperty(nameof(Order.User))]
        public virtual ICollection<Order> Orders { get; set; }
        [InverseProperty(nameof(Wallet.User))]
        public virtual ICollection<Wallet> Wallets { get; set; }
    }
}
