﻿namespace CQRS_MediatR.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        //public virtual ICollection<Order> Order { get; set; }
    }
}
