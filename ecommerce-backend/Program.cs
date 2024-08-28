using ecommerce_backend.Data;
using ecommerce_backend.Interfaces;
using ecommerce_backend.Mappers;
using ecommerce_backend.Models;
using ecommerce_backend.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Configuration files
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Services
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Serialization enum from int to string
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Automappers
builder.Services.AddAutoMapper(typeof(UserProfile));

// Custom services
builder.Services.AddScoped<IUserService, UserService>();

// DbContext for EFC (PostgreSQL) & mapping User.Role enum
var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("DefaultConnection"));
dataSourceBuilder.MapEnum<User.Role>();
var dataSource = dataSourceBuilder.Build();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(dataSource));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();