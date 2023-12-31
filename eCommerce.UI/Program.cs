using Blazored.LocalStorage;
using Blazored.SessionStorage;
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
    builder.Services.AddScoped<FilterRenderingService>();
    builder.Services.AddSingleton<UIService>();
    builder.Services.AddHttpClient<FilterHttpClient>();
    builder.Services.AddHttpClient<ProductHttpClient>();
    //builder.Services.AddBlazoredLocalStorageAsSingleton();
    //builder.Services.AddBlazoredSessionStorageAsSingleton();
}

void ConfigureAutoMapper()
{
    var config = new MapperConfiguration(cfg =>
    {
        cfg.CreateMap<CategoryGetDTO, LinkOption>();
        cfg.CreateMap<FilterGetDTO, FilterGroup>()
           .ForMember(dest => dest.FilterOptions, act => act.MapFrom(src => src.Options));

        /*cfg.CreateMap<CategoryGetDTO, LinkOption>();
        cfg.CreateMap<FilterGetDTO, FilterGroup>();*/
        cfg.CreateMap<OptionGetDTO, FilterOption>().ReverseMap(); ;
    });
    var mapper = config.CreateMapper();
    builder.Services.AddSingleton(mapper);
}
