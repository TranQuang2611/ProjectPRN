using System;
using System.Collections.Generic;

#nullable disable

namespace WheyStoreVN.Models
{
    public partial class FavoriteProduct
    {
        public int CustomerId { get; set; }
        public int DetailId { get; set; }
        public bool? Status { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ProductDetail Detail { get; set; }
    }
}
