namespace TeaStoreApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Detail { get; set; }
        public double Price { get; set; }
        public bool isTrending { get; set; }
        public bool isBestSelling { get; set; }
        public int CategoryId { get; set; }

        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
