using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Consul;
using Ocelot.Provider.Consul; // Add this for Consul integration

var builder = WebApplication.CreateBuilder(args);

// Add Ocelot configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Register Ocelot and Consul services
builder.Services.AddOcelot().AddConsul(); // AddConsul for service discovery

builder.Services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(config =>
{
    config.Address = new Uri("http://localhost:8500"); // Consul agent address
}));

var app = builder.Build();

// Use Ocelot middleware
await app.UseOcelot();

app.Run();
