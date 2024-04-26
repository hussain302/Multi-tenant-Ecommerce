namespace Domain.Entities;
public class Customer : User
{
    public string ShippingAddress { get; set; } = string.Empty;
    public string BillingAddress { get; set; } = string.Empty;
    //orders
}
