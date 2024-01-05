namespace eCommerce.UI.Services;

public class CartService
{
    private readonly LocalStorageService _storage;

    public List<CartItemDTO> CartItems { get; private set; } = [];
    CartItemDTO CurrentCartItem { get; set; } = new();

    public CartService(LocalStorageService storage) => _storage = storage;

    public void SelectColor(ProductGetDTO product, ColorGetDTO color)
    {
        if (product.Id != CurrentCartItem.ProductId)
            CurrentCartItem = new(product.Id, product.Name, product.Description, product.PictureUrl);

        CurrentCartItem.Color = color;

        product.Colors?.ForEach(c => c.IsSelected = false);
        color.IsSelected = true;
    }

    public void SelectSize(ProductGetDTO product, SizeGetDTO size)
    {
        if (product.Id != CurrentCartItem.ProductId)
            CurrentCartItem = new(product.Id, product.Name, product.Description, product.PictureUrl);

        CurrentCartItem.Size = size;
    }

    public async Task AddToCart()
    {
        // Clear any error message
        if (CurrentCartItem.ProductId == 0 || CurrentCartItem.Color.Id == 0 || CurrentCartItem.Size.Id == 0)
            throw new ArgumentException("Cart item not valid");

        CartItems.Add(CurrentCartItem);
        CurrentCartItem = new();

        await SaveCart();
    }

    public async Task SaveCart() => await _storage.SetAsync("Cart", CartItems);
    public async Task GetToCart() => CartItems = await _storage.GetAsync< List<CartItemDTO>>("Cart") ?? [];
    public async Task RemoveFromCart(CartItemDTO item)
    {
        CartItems.Remove(item);
        await SaveCart();
    }
}
