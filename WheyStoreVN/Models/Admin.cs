using System;
using System.Collections.Generic;

#nullable disable

namespace WheyStoreVN.Models
{
    public partial class Admin
    {
        public int AdminId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Phone { get; set; }

        public virtual Account AdminNavigation { get; set; }
    }
}
