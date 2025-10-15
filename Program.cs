using Microsoft.OpenApi.Models;
using BatteryApi;

var builder = WebApplication.CreateBuilder(args);

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Battery API",
        Version = "v1",
        Description = "A simple telemetry API for batteries"
    });
});

// Add repository
builder.Services.AddSingleton<BatteryRepository>();

var app = builder.Build();

// Enable Swagger middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Battery API v1");
});

// Map endpoints
app.MapGet("/batteries", (BatteryRepository repo) => repo.GetAll());
app.MapGet("/batteries/{id}", (BatteryRepository repo, string id) =>
{
    var battery = repo.Get(id);
    return battery is not null ? Results.Ok(battery) : Results.NotFound();
});
app.MapPost("/batteries", (BatteryRepository repo, Battery battery) =>
{
    repo.Add(battery);
    return Results.Created($"/batteries/{battery.Id}", battery);
});
app.MapPost("/batteries/random", (BatteryRepository repo) =>
{
    var b = repo.AddRandom();
    return Results.Created($"/batteries/{b.Id}", b);
});
app.MapDelete("/batteries/{id}", (BatteryRepository repo, string id) =>
{
    return repo.Delete(id) ? Results.Ok() : Results.NotFound();
});

app.Run();
