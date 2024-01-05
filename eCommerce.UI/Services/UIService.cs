namespace eCommerce.UI.Services;

public class UIService
{
    private readonly FilterHttpClient _filterHttp;
    private ProductHttpClient _productHttp;
    private readonly CategoryHttpClient _categoryHttp;
    private readonly IMapper _mapper;
    public CartService Cart { get; }

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
        {
            CurrentCategoryId = linkOption.Id;
            await OnLinkClick(CurrentCategoryId);
        }
    }

    public async Task OnLinkClick(int id)
    {
        CurrentCategoryId = id;
        await GetProductsAsync();
        CaregoryLinkGroups[0].LinkOptions.ForEach(l => l.IsSelected = false);
        CaregoryLinkGroups[0].LinkOptions.Single(l => l.Id.Equals(CurrentCategoryId)).IsSelected = true;

        var filters = Categories.Single(c => c.Id.Equals(CurrentCategoryId)).Filters;
        FilterGroups = _mapper.Map<List<FilterGroup>>(filters);
    }

    public async Task GetProductsAsync() => Products = await _productHttp.GetProductsAsync(CurrentCategoryId);
    
/*    public async Task FilterProducts()
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
    }*/

    public async Task FilterProducts()
    {
        var filterDTOs = FilterGroups
            .Where(group => group.FilterOptions.Any(option => option.OptionType == group.OptionType && option.IsSelected))
            .Select(group => new FilterRequestDTO
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

        if(filterDTOs.Count > 0)
            Products = await _filterHttp.FilterProductsAsync(filterDTOs);
    }

}
