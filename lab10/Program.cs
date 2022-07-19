using lab10.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.Configure<RequestLocalizationOptions>(options => {
//    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-US");
//    options.SupportedCultures = new List<CultureInfo> { new CultureInfo("en-US") };
//    options.RequestCultureProviders.Clear();
//    });
builder.Services.AddControllersWithViews();
builder.Services.AddDbContextPool<MyDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDb")));

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
