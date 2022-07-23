using System;
using System.Collections.Generic;

#nullable disable

namespace WheyStoreVN.Models
{
    public partial class SubCategory
    {
        public SubCategory()
        {
            Products = new HashSet<Product>();
        }

        public int SubCatId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CatId { get; set; }
        public bool? Status { get; set; }

        public virtual Category Cat { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
