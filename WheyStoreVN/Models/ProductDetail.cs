using System;
using System.Collections.Generic;

#nullable disable

namespace WheyStoreVN.Models
{
    public partial class ProductDetail
    {
        public ProductDetail()
        {
            FavoriteProducts = new HashSet<FavoriteProduct>();
            OrderDetails = new HashSet<OrderDetail>();
            ProductSavors = new HashSet<ProductSavor>();
        }

        public int DetailId { get; set; }
        public int? ProductId { get; set; }
        public int? SizeId { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public string Serving { get; set; }
        public string ServingSize { get; set; }
        public string Description { get; set; }
        public int? DefaultPrice { get; set; }
        public int? SellPrice { get; set; }
        public bool? Status { get; set; }

        public virtual Product Product { get; set; }
        public virtual Size Size { get; set; }
        public virtual ICollection<FavoriteProduct> FavoriteProducts { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ProductSavor> ProductSavors { get; set; }
    }
}
