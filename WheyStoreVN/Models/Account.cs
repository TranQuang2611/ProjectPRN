using System;
using System.Collections.Generic;

#nullable disable

namespace WheyStoreVN.Models
{
    public partial class Account
    {
        public int AccountId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? EmailVerify { get; set; }
        public int? Role { get; set; }
        public bool? Status { get; set; }

        public virtual Role RoleNavigation { get; set; }
        public virtual Admin Admin { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
