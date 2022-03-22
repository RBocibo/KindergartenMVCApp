using kindergarten.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using kindergarten.Data;
using System.Configuration;


var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddRazorPages();



builder.Services.AddDbContext<kindergartenDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});


//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<kindergartenContext>(); 
//builder.Services.AddDbContext<kindergartenContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("kindergartenContextConnection")));

//Db Context for identity

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<kindergartenContext>();

builder.Services.AddDbContext<kindergartenContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("kindergartenContextConnection")));

//builder.Services.AddTransient<IDataService, DataService>();

builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
