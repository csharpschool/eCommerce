using eCommerce.UI.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

RegisterServices(builder.Services);
//builder.Services.AddHttpClient<FilterHttpClient>(client => client.BaseAddress = new Uri("https://localhost:5501/api/"));

await builder.Build().RunAsync();


void RegisterServices(IServiceCollection services)
{
    ConfigureAutoMapper(services);
    services.AddScoped<FilterRenderingService>();
    services.AddSingleton<UIService>();
    services.AddHttpClient<FilterHttpClient>();
}

void ConfigureAutoMapper(IServiceCollection services)
{
    var config = new MapperConfiguration(cfg =>
    {
        cfg.CreateMap<CategoryGetDTO, LinkOption>();
        cfg.CreateMap<FilterGetDTO, FilterGroup>()
           .ForMember(dest => dest.FilterOptions, act => act.MapFrom(src => src.Options));

        /*cfg.CreateMap<CategoryGetDTO, LinkOption>();
        cfg.CreateMap<FilterGetDTO, FilterGroup>();*/
        cfg.CreateMap<OptionGetDTO, FilterOption>();
    });
    var mapper = config.CreateMapper();
    services.AddSingleton(mapper);
}
