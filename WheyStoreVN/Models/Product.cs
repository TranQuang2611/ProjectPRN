using System;
using System.Collections.Generic;

#nullable disable

namespace WheyStoreVN.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductDetails = new HashSet<ProductDetail>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public int? SubCatId { get; set; }
        public int? CountryId { get; set; }
        public int? BrandId { get; set; }
        public bool? Status { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Country Country { get; set; }
        public virtual SubCategory SubCat { get; set; }
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
