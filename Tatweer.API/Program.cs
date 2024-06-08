using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Serilog;
using Tatweer.API.Extensions;
using Tatweer.Core.Common;
using Tatweer.Insrastructure.Data;
using Tatweer.Insrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Tatweer.Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Tatweer.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Add services to the container.
builder.Services.AddMediatR(typeof(Program)); // Register MediatR
builder.Services.AddAutoMapper(typeof(Program)); 

//Register MediatorR services 
builder.Services.AddApplicationServices();

builder.Services.AddInfraServices(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shopping Cart API", Version = "v1" });
});
builder.Services.AddScoped<IProductQuery, ProductQuery>();


// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) // Read configuration from appsettings.json or other sources
    .WriteTo.Console() // Add console sink
    .CreateLogger();

builder.Host.UseSerilog(); // Use Serilog for logging
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shopping Cart API v1"));

}

app.UseHttpsRedirection();


app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// Migrate database
app.MigrateDatabase<TatweerContext>((context, services) =>
{
    // Here you can add any seeding logic if needed.
}).WaitForShutdownAsync(); // Ensure the migration completes before running the app


app.Run();
