using Microsoft.EntityFrameworkCore;
using Practice5_DataAccess.Data.AdoRepositories;
using Practice5_DataAccess.Data.EfRepositories;
using Practice5_DataAccess.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<EFSalesRepository>();
builder.Services.AddTransient<ADOSalesRepository>();
builder.Services.AddTransient<IRepositorySalesFactory, SalesRepositoryFactory>();




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
