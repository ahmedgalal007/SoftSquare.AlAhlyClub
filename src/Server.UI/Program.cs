using SoftSquare.AlAhlyClub.Application;
using SoftSquare.AlAhlyClub.Infrastructure;
using SoftSquare.AlAhlyClub.Infrastructure.Persistence;
using SoftSquare.AlAhlyClub.Server;
using SoftSquare.AlAhlyClub.Server.UI;


var builder = WebApplication.CreateBuilder(args);

builder.RegisterSerilog();
builder.WebHost.UseStaticWebAssets();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddServer(builder.Configuration)
    .AddServerUI(builder.Configuration);

var app = builder.Build();

app.ConfigureServer(builder.Configuration);

if (app.Environment.IsDevelopment())
    // Initialise and seed database
    using (var scope = app.Services.CreateScope())
    {
        var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
        await initializer.InitialiseAsync();
        await initializer.SeedAsync();
       
    }

await app.RunAsync();