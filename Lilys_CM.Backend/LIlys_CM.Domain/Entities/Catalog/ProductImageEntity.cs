namespace Lilys_CM.Domain.Entities;

public class ProductImageEntity
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    public ProductEntity Product { get; set; } = default!;

    public string ImageUrl { get; set; } = default!;
    public string FileName { get; set; } = default!;

    public bool IsMain { get; set; }
    public int SortOrder { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}