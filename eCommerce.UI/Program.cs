using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

RegisterServices(builder.Services);
builder.Services.AddHttpClient<FilterHttpClient>(client => client.BaseAddress = new Uri("https://localhost:5501/api/"));

await builder.Build().RunAsync();


void RegisterServices(IServiceCollection services)
{
    //ConfigureAutoMapper(builder.Services);
    services.AddScoped<FilterRenderingService>();
    //services.AddHttpClient<FilterHttpClient>();
}