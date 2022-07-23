using System;
using System.Collections.Generic;

#nullable disable

namespace WheyStoreVN.Models
{
    public partial class Customer
    {
        public Customer()
        {
            FavoriteProducts = new HashSet<FavoriteProduct>();
            Orders = new HashSet<Order>();
        }

        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int? CashInWallet { get; set; }
        public string ImageUrl { get; set; }

        public virtual Account CustomerNavigation { get; set; }
        public virtual ICollection<FavoriteProduct> FavoriteProducts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
