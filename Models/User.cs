namespace TeaStoreApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
        ICollection<Order> Orders { get; set; }
        public string Role { get; set; } = "Users";
    }
}
