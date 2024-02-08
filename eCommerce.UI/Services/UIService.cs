namespace eCommerce.UI.Services;

public class UIService(FilterHttpClient filterHttp, ProductHttpClient productHttp, CategoryHttpClient categoryHttp, IMapper mapper, CartService cart)
{
    public CartService Cart { get; } = cart;

    List<CategoryGetDTO> Categories { get; set; } = [];
    public List<ProductGetDTO> Products { get; private set; } = [];
    public List<FilterGroup> FilterGroups { get; private set; } = [];
    public List<LinkGroup> CaregoryLinkGroups { get; private set; } =
    [
        new LinkGroup { Name = "Categories" }
    ];
    public int CurrentCategoryId { get; set; }

    public async Task GetLinkGroup()
    {
        Categories = await categoryHttp.GetCategoriesAsync();
        CaregoryLinkGroups[0].LinkOptions = mapper.Map<List<LinkOption>>(Categories);
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
        FilterGroups = mapper.Map<List<FilterGroup>>(filters);
    }

    public async Task GetProductsAsync() => Products = await productHttp.GetProductsAsync(CurrentCategoryId);

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
                    .Select(option => mapper.Map<OptionDTO>(option))
                    .ToList()
            }).ToList();

        if(filterDTOs.Count > 0)
            Products = await filterHttp.FilterProductsAsync(filterDTOs);
    }

}
