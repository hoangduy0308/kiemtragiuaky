using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using MyMvcApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Thêm dịch vụ Razor + MVC
builder.Services.AddControllersWithViews();

// Cấu hình DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDbConnection")));

// Đăng ký IWebHostEnvironment để dùng trong controller
builder.Services.AddSingleton<IWebHostEnvironment>(builder.Environment);

// Thêm Session
builder.Services.AddDistributedMemoryCache(); // cần cho session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // thời gian timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Cấu hình xác thực bằng cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/SinhVien/Login";   // đường dẫn trang đăng nhập
        options.LogoutPath = "/SinhVien/Logout"; // nếu bạn có logout
    });

var app = builder.Build();

// Cấu hình middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Kích hoạt session và authentication
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

// Cấu hình route mặc định
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=SinhVien}/{action=Index}/{id?}");

app.Run();
