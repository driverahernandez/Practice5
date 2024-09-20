using Practice5_DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Practice5_DataAccess.Interface;
using Practice5_DataAccess.Data.EfRepositories;
using Practice5_DataAccess.Data.AdoRepositories;
using Microsoft.AspNetCore.Authentication;
using Practice5_Web;

var builder = WebApplication.CreateBuilder(args);

// Changes below were made for dependency injection of class SalesController.
// I used code from https://stackoverflow.com/questions/40654801/register-service-at-runtime-via-di as reference
// to be able to switch from one repository to the other while the website app is running
// and to use dependency injection in the constructor of the SalesController
// (or any of the other controllers, if the same changes are repeated there. For lack of time I only made changes to SalesController,
// as it is the class I'll be testing.)

// The previous method coded the switch within the constructor depending on AccesType id, as can be seen in the other controllers. 
// this change was necessary so I could pass a mock sales repository to the SalesController constructor in the Test Project.

builder.Services.AddTransient<EFSalesRepository>();
builder.Services.AddTransient<ADOSalesRepository>();
builder.Services.AddTransient<IRepositorySalesFactory, SalesRepositoryFactory>();


// Add services to the container.
builder.Services.AddControllersWithViews();

//Configure DbContext into web project.
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
//});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
