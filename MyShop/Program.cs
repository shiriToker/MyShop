using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repository;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IMyRepository, MyRepository>();
builder.Services.AddScoped<IMyService, MyService>();
builder.Services.AddDbContext<MyShop328306782Context>(Options => Options.UseSqlServer("Server=SRV2\\PUPILS;Database=MyShop_328306782;Trusted_Connection=True;TrustServerCertificate=True"));



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
