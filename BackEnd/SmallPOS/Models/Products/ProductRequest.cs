namespace SmallPOS.API.Models.Products;

public class ProductRequest
{
    public string Name { get; set; } = string.Empty;

    public string SKU { get; set; } = string.Empty;

    public int? CategoryId { get; set; }

    public decimal PurchasePrice { get; set; }

    public decimal SalePrice { get; set; }

    public int Stock { get; set; }

    public string Status { get; set; } = "Active";
}
