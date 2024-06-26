namespace TeaStoreApi.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
        public double Price { get; set; }
        public double TotalAmount { get; set; }
    }
}
