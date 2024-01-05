namespace eCommerce.API.DTO;
public class CartItemDTO : ProductPostDTO
{
    public int ProductId { get; set; }
    public SizeGetDTO Size { get; set; } = new();
    public ColorGetDTO Color { get; set; } = new();

    public CartItemDTO() { }
    public CartItemDTO(int productId, string productName, string description, string pictureUrl) 
        => (ProductId, Name, Description, PictureUrl) = (productId, productName, description, pictureUrl);
}