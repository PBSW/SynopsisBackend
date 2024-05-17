using Application;
using Application.Interfaces;
using FluentValidation;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

// Dependency Injections
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();

builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Database
builder.Services.AddDbContext<DatabaseContext>(options => options.UseMySql(
    builder.Configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
