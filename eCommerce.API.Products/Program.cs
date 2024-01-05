using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SQL Server Service Registration
builder.Services.AddDbContext<ECommerceContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("ECommerceConnection")));

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("CorsAllAccessPolicy", opt =>
        opt.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod()
    );
});

RegisterServices(builder.Services);

var app = builder.Build();

RegisterEndpoints(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Configure CORRS
app.UseCors("CorsAllAccessPolicy");

app.Run();

void RegisterServices(IServiceCollection services)
{
    ConfigureAutoMapper(builder.Services);
    services.AddScoped<IDbService, ProductDbService>();
}

void RegisterEndpoints(WebApplication app)
{
    app.AddEndpoint<Product, ProductPostDTO, ProductPutDTO, ProductGetDTO>();
    app.AddEndpoint<Size, SizePostDTO, SizePutDTO, SizeGetDTO>();
    app.AddEndpoint<Color, ColorPostDTO, ColorPutDTO, ColorGetDTO>();
    app.AddEndpoint<ProductCategory, ProductCategoryPostDTO, ProductCategoryDeleteDTO>();
    app.AddEndpoint<ProductSize, ProductSizePostDTO, ProductSizeDeleteDTO>();
    app.AddEndpoint<ProductColor, ProductColorPostDTO, ProductColorDeleteDTO>();
    app.MapGet($"/api/productsbycategory/" + "{categoryId}", async (IDbService db, int categoryId) =>
    {
        try
        {
            return Results.Ok(await ((ProductDbService)db).GetProductsByCategoryAsync(categoryId));
        }
        catch
        {
        }

        return Results.BadRequest($"Couldn't get the requested products of type {typeof(Product).Name}.");
    });
}

void ConfigureAutoMapper(IServiceCollection services)
{
    var config = new MapperConfiguration(cfg =>
    {
        cfg.CreateMap<Product, ProductPostDTO>().ReverseMap();
        cfg.CreateMap<Product, ProductPutDTO>().ReverseMap();
        cfg.CreateMap<Product, ProductGetDTO>().ReverseMap();
        cfg.CreateMap<Size, SizePostDTO>().ReverseMap();
        cfg.CreateMap<Size, SizePutDTO>().ReverseMap();
        cfg.CreateMap<Size, SizeGetDTO>().ReverseMap();
        cfg.CreateMap<Color, ColorPostDTO>().ReverseMap();
        cfg.CreateMap<Color, ColorPutDTO>().ReverseMap();
        cfg.CreateMap<Color, ColorGetDTO>().ReverseMap();
        cfg.CreateMap<ProductCategory, ProductCategoryPostDTO>().ReverseMap();
        cfg.CreateMap<ProductCategory, ProductCategoryDeleteDTO>().ReverseMap();
        cfg.CreateMap<ProductSize, ProductSizePostDTO>().ReverseMap();
        cfg.CreateMap<ProductSize, ProductSizeDeleteDTO>().ReverseMap();
        cfg.CreateMap<ProductColor, ProductColorPostDTO>().ReverseMap();
        cfg.CreateMap<ProductColor, ProductColorDeleteDTO>().ReverseMap();
    });
    var mapper = config.CreateMapper();
    services.AddSingleton(mapper);
}