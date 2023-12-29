var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SQL Server Service Registration
builder.Services.AddDbContext<FilterContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("ECommerceConnection")));

builder.Services.AddCors(policy => {
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

app.Run();

void RegisterServices(IServiceCollection services)
{
    ConfigureAutoMapper(builder.Services);
    services.AddScoped<IDbService, FilterDbService<FilterContext>>();
}

void RegisterEndpoints(WebApplication app)
{
    app.AddEndpoint<FilterContext, Category, CategoryPostDTO, CategoryPutDTO, CategoryGetDTO>();
    app.AddEndpoint<FilterContext, Filter, FilterPostDTO, FilterPutDTO, FilterGetDTO>();
    app.AddEndpoint<FilterContext, Option, OptionPostDTO, OptionPutDTO, OptionGetDTO>();
    app.AddEndpoint<FilterContext, CategoryFilter, CategoryFilterPostDTO, CategoryFilterDeleteDTO>();
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
        cfg.CreateMap<Option, OptionPostDTO>().ReverseMap();
        cfg.CreateMap<Option, OptionPutDTO>().ReverseMap();
        cfg.CreateMap<Option, OptionGetDTO>().ReverseMap();
        cfg.CreateMap<CategoryFilter, CategoryFilterPostDTO>().ReverseMap();
        cfg.CreateMap<CategoryFilter, CategoryFilterDeleteDTO>().ReverseMap();
        //cfg.CreateMap<FilterOption, FilterOptionPostDTO>().ReverseMap();
        //cfg.CreateMap<FilterOption, FilterOptionDeleteDTO>().ReverseMap();
    });
    var mapper = config.CreateMapper();
    services.AddSingleton(mapper);
}
