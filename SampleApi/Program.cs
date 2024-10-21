using Microsoft.EntityFrameworkCore;
using SampleApi.Core.DataAccess.Context;
using SampleApi.Logic.Products;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IGetProductListService, GetProductListService>();
builder.Services.AddScoped<IGetProductService, GetProductService>();
builder.Services.AddScoped<IAddProductService, AddProductService>();
builder.Services.AddScoped<IUpdateProductService, UpdateProductService>();
builder.Services.AddScoped<IDeleteProductService, DeleteProductService>();
builder.Services.AddDbContextPool<SampleDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"),
    o => o.SetPostgresVersion(16, 3)));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
