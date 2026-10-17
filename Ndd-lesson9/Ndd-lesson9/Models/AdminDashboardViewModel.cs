namespace Ndd_lesson9.Models;

public class AdminDashboardViewModel
{
    public int CategoryCount { get; init; }
    public int ProductCount { get; init; }
    public int BannerCount { get; init; }
    public int BlogCount { get; init; }
    public IReadOnlyList<Product> RecentProducts { get; init; } = [];
}
