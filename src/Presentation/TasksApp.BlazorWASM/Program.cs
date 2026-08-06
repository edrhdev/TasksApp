using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using TasksApp.BlazorWASM;
using TasksApp.BlazorWASM.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiBaseUrl = builder.Configuration["ApiConfig:BaseUrl"];

if (string.IsNullOrWhiteSpace(apiBaseUrl))
    throw new InvalidOperationException("API base URL is not configured. Please check the configuration settings.");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(apiBaseUrl)
});

builder.Services.AddScoped<ITaskApiClient, TaskApiClient>();

builder.Services.AddRadzenComponents();

await builder.Build().RunAsync();
