﻿namespace TeaStoreApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public double OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
        public int UserId { get; set; }
        ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
