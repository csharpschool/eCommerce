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
    services.AddScoped<IDbService, FilterDbService>();
}

void RegisterEndpoints(WebApplication app)
{
    app.AddEndpoint<Category, CategoryPostDTO, CategoryPutDTO, CategoryGetDTO>();
    app.AddEndpoint<Filter, FilterPostDTO, FilterPutDTO, FilterGetDTO>();
    app.AddEndpoint<FilterType, FilterTypePostDTO, FilterTypePutDTO, FilterTypeGetDTO>();
    app.AddEndpoint<Option, OptionPostDTO, OptionPutDTO, OptionGetDTO>();
    app.AddEndpoint<CategoryFilter, CategoryFilterPostDTO, CategoryFilterDeleteDTO>();
    /*app.MapPost("/api/filterproducts", async (List<FilterRequestDTO> filterDTOs, IFilterService filterService) =>
    {
        try
        {
            // Assuming filterService.ProcessFiltering() is your method to apply filters
            var filterDTOs = filterService.ProcessFiltering(filterRequest);
            return Results.Ok(filterDTOs);
        }
        catch (Exception ex)
        {
            // Log the exception details and return an appropriate error response
            return Results.Problem(ex.Message);
        }
    });*/
    app.MapPost("/api/filterproducts", async (List<FilterRequestDTO> filterDTOs) =>
    {
        try
        {
            //TODO: Implement a filtering service
            // See commented out code above for injection example
            return Results.Ok();
        }
        catch (Exception ex)
        {
            // Log the exception details and return an appropriate error response
            return Results.Problem(ex.Message);
        }
    });

}

void ConfigureAutoMapper(IServiceCollection services)
{
    var config = new MapperConfiguration(cfg =>
    {
        cfg.CreateMap<Category, CategoryPostDTO>().ReverseMap();
        cfg.CreateMap<Category, CategoryPutDTO>().ReverseMap();
        cfg.CreateMap<Category, CategoryGetDTO>().ReverseMap();
        cfg.CreateMap<Filter, FilterPostDTO>().ReverseMap();
        cfg.CreateMap<Filter, FilterPutDTO>().ReverseMap();
        cfg.CreateMap<Filter, FilterGetDTO>().ReverseMap();
        cfg.CreateMap<FilterType, FilterTypePostDTO>().ReverseMap();
        cfg.CreateMap<FilterType, FilterTypePutDTO>().ReverseMap();
        cfg.CreateMap<FilterType, FilterTypeGetDTO>().ReverseMap();
        cfg.CreateMap<Option, OptionPostDTO>().ReverseMap();
        cfg.CreateMap<Option, OptionPutDTO>().ReverseMap();
        cfg.CreateMap<Option, OptionGetDTO>().ReverseMap();
        cfg.CreateMap<CategoryFilter, CategoryFilterPostDTO>().ReverseMap();
        cfg.CreateMap<CategoryFilter, CategoryFilterDeleteDTO>().ReverseMap();
    });
    var mapper = config.CreateMapper();
    services.AddSingleton(mapper);
}
