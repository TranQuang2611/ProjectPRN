using System;
using System.Collections.Generic;

#nullable disable

namespace WheyStoreVN.Models
{
    public partial class Country
    {
        public Country()
        {
            Products = new HashSet<Product>();
        }

        public int CountryId { get; set; }
        public string Name { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
