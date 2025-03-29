using InventoryManagement.Repository;
using IventoryManagement.Api.Configuration;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Use in-memory SQLite database
builder.Services.AddDbContext<InventoryManagementDbContext>(options =>
    options.UseSqlite("DataSource=InventoryManagement.db"));

builder.Services
    .AddMappers()
    .AddMyDependencyGroup();

var app = builder.Build();

// Ensure the database is created and migrations are applied
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<InventoryManagementDbContext>();
    dbContext.Database.OpenConnection();
    dbContext.Database.Migrate();
}

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