using System;
using System.Collections.Generic;

#nullable disable

namespace WheyStoreVN.Models
{
    public partial class OrderDetail
    {
        public int OrderId { get; set; }
        public int DetailId { get; set; }
        public int SavorId { get; set; }
        public int? Quantity { get; set; }
        public string Description { get; set; }

        public virtual ProductDetail Detail { get; set; }
        public virtual Order Order { get; set; }
        public virtual Savor Savor { get; set; }
    }
}
