namespace Domain.Entities;

public class ShoppingCartItem
{
    public Guid Id { get; set; }
    public Guid ShoppingCartId { get; set; }
    public virtual ShoppingCart? ShoppingCart { get; set; }
    public Guid ProductId { get; set; }//Soft_fk
    public int Quantity { get; set; }
}