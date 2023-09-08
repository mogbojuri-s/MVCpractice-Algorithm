using FirstMVCSQ016.Data;
using FirstMVCSQ016.Data.Entities;
using FirstMVCSQ016.Models;
using FirstMVCSQ016.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanAdd", policy => policy.RequireClaim("CanAdd", new List<string> { "true" }));
    options.AddPolicy("CanEdit", policy => policy.RequireClaim("CanEdit", new List<string> { "true" }));
    options.AddPolicy("CanDelete", policy => policy.RequireClaim("CanDelete", new List<string> { "true" }));
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<MVCContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("default")));
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<MVCContext>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();







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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
