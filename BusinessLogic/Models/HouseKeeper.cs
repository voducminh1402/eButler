using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BusinessLogic.Models
{
    [Table("HouseKeeper")]
    public partial class HouseKeeper
    {
        [Key]
        [StringLength(50)]
        public string Id { get; set; }
        [StringLength(100)]
        public string FullName { get; set; }
        [Required]
        [StringLength(10)]
        public string Phone { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [StringLength(50)]
        public string Gender { get; set; }
        [StringLength(100)]
        public string Image { get; set; }

        [ForeignKey(nameof(Id))]
        [InverseProperty(nameof(User.HouseKeeper))]
        public virtual User IdNavigation { get; set; }
    }
}
