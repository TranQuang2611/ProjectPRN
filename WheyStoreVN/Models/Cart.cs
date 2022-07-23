namespace WheyStoreVN.Models
{
    public class Cart
    {
        public ProductSavor Product { get; set; }
        public int Quantity { get; set; }

        public Cart()
        {
        }


        public Cart(ProductSavor product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public int? TotalPrice()
        {
            return Product.Detail.SellPrice * Quantity;
        }
    }
}
