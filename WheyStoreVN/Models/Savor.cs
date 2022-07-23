using System;
using System.Collections.Generic;

#nullable disable

namespace WheyStoreVN.Models
{
    public partial class Savor
    {
        public Savor()
        {
            OrderDetails = new HashSet<OrderDetail>();
            ProductSavors = new HashSet<ProductSavor>();
        }

        public int SavorId { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ProductSavor> ProductSavors { get; set; }
    }
}
