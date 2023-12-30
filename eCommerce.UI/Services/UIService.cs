using AutoMapper;
using Microsoft.AspNetCore.Components.Web;

namespace eCommerce.UI.Services;

public class UIService
{
    private readonly FilterHttpClient _filterHttp;
    private readonly ProductHttpClient _productHttp;
    private readonly IMapper _mapper;

    List<CategoryGetDTO> Categories { get; set; } = [];
    public List<ProductGetDTO> Products { get; private set; } = [];
    public List<FilterGroup> FilterGroups { get; private set; } = [];
    public LinkGroup LinkGroup { get; private set; } = new()
    {
        Name = "Categories"
    };

    public UIService(FilterHttpClient filterHttp, ProductHttpClient productHttp, IMapper mapper)
    {
        _filterHttp = filterHttp;
        _productHttp = productHttp;
        _mapper = mapper;
    }

    public async Task GetLinkGroup()
    {
        Categories = await _filterHttp.GetCategoriesAsync();
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

    public async Task GetProductsAsync(int categoryId)
    {
        Products = await _productHttp.GetProductsAsync(categoryId);
    }

    public async Task FilterProducts()
    {
        // Use the filters collection
    }

}
