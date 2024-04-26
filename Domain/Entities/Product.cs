using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }

    [ForeignKey("VendorId")]
    public Guid VendorId { get; set; }
    public virtual Vendor? Vendor { get; set; }   
    
    [ForeignKey("CategoryId")]
    public Guid CategoryId { get; set; }
    public virtual Category? Category { get; set; }

    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
}