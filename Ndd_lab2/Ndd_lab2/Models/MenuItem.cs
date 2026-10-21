namespace Ndd_lab2.Models;

public class MenuItem
{
    public string Title { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Section { get; set; } = string.Empty;
    public List<MenuItem> Children { get; set; } = new();
}
