using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Library_API_repeat.Client;
using Library_API_repeat.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5091") });

builder.Services.AddScoped<AuthenticationService>();

var host = builder.Build();

var authenticationService = host.Services.GetRequiredService<AuthenticationService>();

await authenticationService.InitializeAsync();

await host.RunAsync();
