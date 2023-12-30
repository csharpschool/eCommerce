using AutoMapper;

namespace eCommerce.UI.Services;

public class UIService
{
    private readonly FilterHttpClient _http;
    private readonly IMapper _mapper;

    List<CategoryGetDTO> Categories { get; set; } = [];
    public List<FilterGroup> FilterGroups { get; private set; } = [];
    public LinkGroup LinkGroup { get; private set; } = new()
    {
        Name = "Categories"
    };

    public UIService(FilterHttpClient http, IMapper mapper)
    {
        _http = http;
        _mapper = mapper;
    }

    public async Task GetLinkGroup()
    {
        Categories = await _http.GetCategoriesAsync();
        LinkGroup.LinkOptions = _mapper.Map<List<LinkOption>>(Categories);
        var linkOption = LinkGroup.LinkOptions.FirstOrDefault();

        if (linkOption is not null)
            OnLinkClick(linkOption.Id);
    }

    public void OnLinkClick(int id)
    {
        LinkGroup.LinkOptions.ForEach(l => l.IsSelected = false);
        LinkGroup.LinkOptions.Single(l => l.Id.Equals(id)).IsSelected = true;

        var filters = Categories.Single(c => c.Id.Equals(id)).Filters;
        FilterGroups = _mapper.Map<List<FilterGroup>>(filters);
    }
}
