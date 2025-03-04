using Microsoft.EntityFrameworkCore;
using MyShop.Middlewares;
using NLog.Web;
using PresidentsApp.Middlewares;
using Repository;
using Services;

var builder = WebApplication.CreateBuilder(args);

string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

string connectionString;

if (environment == "Home")
{
    connectionString = builder.Configuration.GetConnectionString("HomeConnection");
}
else if (environment == "School")
{
    connectionString = builder.Configuration.GetConnectionString("SchoolConnection");
}
else if (environment == "Development")
{
    connectionString = builder.Configuration.GetConnectionString("SchoolConnection");
}
else
{
    throw new Exception("Unknown environment");
}

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IMyRepository, MyRepository>();
builder.Services.AddScoped<IMyService, MyService>();
builder.Services.AddScoped<IcategoryRepository, categoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddDbContext<MyShop328306782Context>(Options => Options.UseSqlServer(connectionString));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Host.UseNLog();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Configure the HTTP request pipeline.

app.UseErrorHandlingMiddleware();

app.UseRatingMiddleware();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();