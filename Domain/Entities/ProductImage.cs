using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public class ProductImage
{
    public Guid Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool? IsPrimary { get; set; }
    
    [ForeignKey("ProductId")]
    public Guid ProductId { get; set; }
    public virtual Product? Product { get; set; }
}
