namespace eCommerce.UI.Services;

public class UIService
{
    private readonly FilterHttpClient _filterHttp;
    private readonly ProductHttpClient _productHttp;
    private readonly CategoryHttpClient _categoryHttp;
    private readonly IMapper _mapper;
    public CartService Cart { get; } = new();
    

    List<CategoryGetDTO> Categories { get; set; } = [];
    public List<ProductGetDTO> Products { get; private set; } = [];
    public List<FilterGroup> FilterGroups { get; private set; } = [];
    public List<LinkGroup> CaregoryLinkGroups { get; private set; } =
    [
        new LinkGroup { Name = "Categories" }
    ];
    public int CurrentCategoryId { get; set; }

    public UIService(FilterHttpClient filterHttp, ProductHttpClient productHttp, CategoryHttpClient categoryHttp, IMapper mapper, CartService cart)
    {
        _filterHttp = filterHttp;
        _productHttp = productHttp;
        _categoryHttp = categoryHttp;
        _mapper = mapper;
        Cart = cart;
    }

    public async Task GetLinkGroup()
    {
        Categories = await _categoryHttp.GetCategoriesAsync();
        CaregoryLinkGroups[0].LinkOptions = _mapper.Map<List<LinkOption>>(Categories);
        var linkOption = CaregoryLinkGroups[0].LinkOptions.FirstOrDefault();

        if (linkOption is not null)
            OnLinkClick(linkOption.Id);
    }

    public void OnLinkClick(int id)
    {
        CaregoryLinkGroups[0].LinkOptions.ForEach(l => l.IsSelected = false);
        CaregoryLinkGroups[0].LinkOptions.Single(l => l.Id.Equals(id)).IsSelected = true;

        CurrentCategoryId = id;
        var filters = Categories.Single(c => c.Id.Equals(id)).Filters;
        FilterGroups = _mapper.Map<List<FilterGroup>>(filters);
    }

    public async Task GetProductsAsync(int categoryId)
    {
        Products = await _productHttp.GetProductsAsync(categoryId);
    }

    public async Task FilterProducts()
    {
        var filterDTOs = FilterGroups.Select(group => new FilterRequestDTO
        {
            CategoryId = CurrentCategoryId,
            OptionType = group.OptionType,
            Id = group.Id,
            Name = group.Name,
            TypeName = group.TypeName,
            Options = group.FilterOptions
                  .Where(option => option.OptionType == group.OptionType && option.IsSelected)
                  .Select(option => _mapper.Map<OptionDTO>(option))
                  .ToList()
        }).ToList();

        Products = await _filterHttp.FilterProductsAsync(filterDTOs);
    }

}
