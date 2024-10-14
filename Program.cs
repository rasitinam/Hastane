using Hastane.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DbContext'i Servislere ekleyin ve ConnectionString'i ayarlayın
builder.Services.AddDbContext<HospitalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDbContextConnectionString")));

// Session'ı ekleyin
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session'ın geçerli olacağı süre
    options.Cookie.HttpOnly = true; // Session çerezlerini sadece HTTP üzerinden erişilebilir kılma
    options.Cookie.IsEssential = true; // Genel site çalışması için gerekli olduğundan zorunlu çerez olarak işaretlenir
});

builder.Services.AddControllersWithViews();
var app = builder.Build();

// Middleware'ler
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession(); // Session middleware'i kullan
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
