namespace eCommerce.API.DTO;

public class ProductColorPostDTO
{
    public int ColorId { get; set; }
    public int ProductId { get; set; }
}
public class ProductColorDeleteDTO : ProductColorPostDTO
{
}
public class ProductColorGetDTO : ProductColorPostDTO
{
}

public class ProductColorSmallGetDTO
{
    public int ColorId { get; set; }
}