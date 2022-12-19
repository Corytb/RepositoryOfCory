namespace CQRS_MediatR.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string BusinessName { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
