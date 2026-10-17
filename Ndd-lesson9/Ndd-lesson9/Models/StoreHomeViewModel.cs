namespace Ndd_lesson9.Models;

public class StoreHomeViewModel
{
    public IReadOnlyList<Category> Categories { get; init; } = [];
    public IReadOnlyList<Product> FeaturedProducts { get; init; } = [];
    public Banner? FeaturedBanner { get; init; }
}
