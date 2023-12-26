namespace eCommerce.Common.Razor.Services.Links;

public class LinkGroup
{
    public string Name { get; set; } = string.Empty;
    public List<LinkOption> LinkOptions { get; set; } = [];
}
