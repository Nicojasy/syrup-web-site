namespace Syrup.Core.Models;

public class CreateProductRequest
    public long Id { get; set; }
    public int Price { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
