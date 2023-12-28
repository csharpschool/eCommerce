using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

await builder.Build().RunAsync();

RegisterServices(builder.Services);

void RegisterServices(IServiceCollection services)
{
    //ConfigureAutoMapper(builder.Services);
    services.AddScoped<FilterRenderingService>();
    services.AddScoped<FilterHttpClient>();
}