using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyShop.Middlewares;
using NLog.Web;
using PresidentsApp.Middlewares;
using Repository;
using Services;

var builder = WebApplication.CreateBuilder(args);
// הוספת Authentication עם JwtBearer
var jwtKey = builder.Configuration["Jwt:Key"];
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            // שליפת הטוקן מה-Cookie
            var token = context.Request.Cookies["jwtToken"];
            if (!string.IsNullOrEmpty(token))
            {
                context.Token = token;
            }
            return Task.CompletedTask;
        }
    };
});

string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

string connectionString;

if (environment == "Home")
{
    connectionString = builder.Configuration.GetConnectionString("HomeConnection");
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
app.UseAuthentication();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Configure the HTTP request pipeline.

app.UseErrorHandlingMiddleware();
app.UseRatingMiddleware();
app.UseMiddleware<MyShop.Middlewares.JwtMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();
app.Run();