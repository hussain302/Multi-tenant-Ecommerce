namespace Domain.Entities;

public class ShoppingCart
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public virtual Customer? Customer { get; set; }
    public virtual ICollection<ShoppingCartItem>? Items { get; set; }
}