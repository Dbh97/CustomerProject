using CustomerProject.Models.Data;
using CustomerProject.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddDbContext<CustomerContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"));
});

// injects services needed for routing (DI for controllers)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// so app can access static files in wwwroot, e.g. bootstrap.
app.UseStaticFiles();

// routing for MVC, middleware + route
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Customers}/{action=Index}/{id?}");

app.Run();