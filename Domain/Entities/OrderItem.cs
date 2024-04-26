namespace Domain.Entities;
public class OrderItem
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public virtual Order? Order { get; set; }
    public Guid ProductId { get; set; } //Soft_fk
    public int Quantity { get; set; }
    public double DiscountOnItem { get; set; }
    public decimal Price { get; set; }
}