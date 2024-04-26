using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    public bool OrderConfirmed { get; set; }
    public decimal TotalAmount { get; set; }
    public string OrderStatus { get; set; } = string.Empty;
    public string ShippingDetail { get; set; } = string.Empty;
    
    [ForeignKey("CustomerId")]
    public Guid CustomerId { get; set; }
    public virtual Customer? Customer { get; set; }
    public virtual ICollection<OrderItem>? OrderItems { get; set; }
}