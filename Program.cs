using api_flutter.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(
    opts =>
    {
        opts.UseSqlServer
        (
            builder
            .Configuration
            .GetConnectionString("AzureConnection")
        );
    }

);

var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
