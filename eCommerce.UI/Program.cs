using Blazored.LocalStorage;
using Blazored.SessionStorage;
using eCommerce.Storage.WebAssembly.Services;
using eCommerce.UI.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

RegisterServices();
//builder.Services.AddHttpClient<FilterHttpClient>(client => client.BaseAddress = new Uri("https://localhost:5501/api/"));

await builder.Build().RunAsync();


void RegisterServices()
{
    ConfigureAutoMapper();
    builder.Services.AddBlazoredLocalStorageAsSingleton();
    builder.Services.AddBlazoredSessionStorageAsSingleton();
    builder.Services.AddSingleton<SessionStorageService>();
    builder.Services.AddSingleton<LocalStorageService>();
    builder.Services.AddScoped<FilterRenderingService>();
    builder.Services.AddSingleton<UIService>();
    builder.Services.AddSingleton<CartService>();
    builder.Services.AddHttpClient<FilterHttpClient>();
    builder.Services.AddHttpClient<ProductHttpClient>();
    builder.Services.AddHttpClient<CategoryHttpClient>(); 
}

void ConfigureAutoMapper()
{
    var config = new MapperConfiguration(cfg =>
    {
        cfg.CreateMap<CategoryGetDTO, LinkOption>();
        cfg.CreateMap<FilterGetDTO, FilterGroup>()
           .ForMember(dest => dest.FilterOptions, act => act.MapFrom(src => src.Options));
        cfg.CreateMap<OptionDTO, FilterOption>().ReverseMap();
        /*
        cfg.CreateMap<FilterGetDTO, FilterGroup>();
        cfg.CreateMap<OptionGetDTO, FilterOption>().ReverseMap();*/
    });
    var mapper = config.CreateMapper();
    builder.Services.AddSingleton(mapper);
}
