using System;
using System.Collections.Generic;

#nullable disable

namespace WheyStoreVN.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? ShipperId { get; set; }
        public string Location { get; set; }
        public int? Amount { get; set; }
        public bool? Status { get; set; }
        public string Description { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Shipper Shipper { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
