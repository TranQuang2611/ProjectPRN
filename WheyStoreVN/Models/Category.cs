using System;
using System.Collections.Generic;

#nullable disable

namespace WheyStoreVN.Models
{
    public partial class Category
    {
        public Category()
        {
            SubCategories = new HashSet<SubCategory>();
        }

        public int CatId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
