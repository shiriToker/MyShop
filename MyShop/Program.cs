using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyShop.Middlewares;
using NLog.Web;
using Repository;
using Services;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddDbContext<MyShop328306782Context>(Options => Options.UseSqlServer("Server=SRV2\\PUPILS;Database=MyShop_328306782;Trusted_Connection=True;TrustServerCertificate=True"));


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();

builder.Host.UseNLog();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Configure the HTTP request pipeline.
app.UseRatingMiddleware();

app.UseHttpsRedirection();



app.UseStaticFiles();



app.UseAuthorization();

app.MapControllers();

app.Run();
