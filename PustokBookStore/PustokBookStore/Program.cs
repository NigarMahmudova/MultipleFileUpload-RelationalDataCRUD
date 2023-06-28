using Microsoft.EntityFrameworkCore;
using PustokBookStore.DAL;
using PustokBookStore.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PustokDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddScoped<LayoutService>();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
    );

app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.Run();
