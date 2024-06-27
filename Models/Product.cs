using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TeaStoreApi.Models
{
    public class Product
    {
        [BindNever]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Detail { get; set; }
        public double Price { get; set; }
        public bool isTrending { get; set; }
        public bool isBestSelling { get; set; }
        public int CategoryId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public IFormFile Image { get; set; }
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
