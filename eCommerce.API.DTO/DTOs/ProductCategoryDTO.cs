namespace eCommerce.API.DTO;

public class ProductCategoryPostDTO
{
    public int CategoryId { get; set; }
    public int ProductId { get; set; }
}
public class ProductCategoryDeleteDTO : ProductCategoryPostDTO
{
}
