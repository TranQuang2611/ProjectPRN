using System;
using System.Collections.Generic;

#nullable disable

namespace WheyStoreVN.Models
{
    public partial class ProductSavor
    {
        public int DetailId { get; set; }
        public int SavorId { get; set; }
        public int? Quantity { get; set; }
        public bool? Status { get; set; }

        public virtual ProductDetail Detail { get; set; }
        public virtual Savor Savor { get; set; }
    }
}
