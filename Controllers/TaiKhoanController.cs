using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace MyMvcApp.Controllers
{
    public class TaiKhoanController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string tenDangNhap, string matKhau)
        {
            if (tenDangNhap == "admin" && matKhau == "123")
            {
                HttpContext.Session.SetString("TenDangNhap", tenDangNhap);
                return RedirectToAction("Index", "SinhVien");
            }

            ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu sai!";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("TenDangNhap");
            return RedirectToAction("Login");
        }
    }
}
