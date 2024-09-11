using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Consul;

var builder = WebApplication.CreateBuilder(args);

// Add Ocelot configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Register Ocelot and Consul services
builder.Services.AddOcelot();
builder.Services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(config =>
{
    config.Address = new Uri("http://localhost:8500");
}));

var app = builder.Build();

await app.UseOcelot();

app.Run();
