using Microsoft.EntityFrameworkCore;
using Practice5_DataAccess.Data.AdoRepositories;
using Practice5_DataAccess.Data.EfRepositories;
using Practice5_DataAccess.Data;
using Practice5_DataAccess.Data.RepositoryFactory;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<EFSalesRepository>();
builder.Services.AddTransient<ADOSalesRepository>();
builder.Services.AddTransient<EFProductsRepository>();
builder.Services.AddTransient<ADOProductsRepository>();
builder.Services.AddTransient<EFPurchasesRepository>();
builder.Services.AddTransient<ADOPurchasesRepository>();
builder.Services.AddTransient<EFProductsInventoryRepository>();
builder.Services.AddTransient<ADOProductsInventoryRepository>();

builder.Services.AddTransient<IRepositorySalesFactory, SalesRepositoryFactory>(); 
builder.Services.AddTransient<IRepositoryProductsFactory, ProductsRepositoryFactory>();
builder.Services.AddTransient<IRepositoryPurchasesFactory, PurchasesRepositoryFactory>();
builder.Services.AddTransient<IRepositoryProductsInventoryFactory, ProductsInventoryRepositoryFactory>();





//Configure DbContext into web project.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
