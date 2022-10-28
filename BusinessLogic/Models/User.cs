using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessLogic.Models
{
    public partial class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string RoleId { get; set; }
        public bool IsSystemAdmin { get; set; }

        public virtual Role Role { get; set; }
        public virtual HouseKeeper HouseKeeper { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
