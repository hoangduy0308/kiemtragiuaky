using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace MyMvcApp.Controllers
{
    public class HocPhanController : Controller
    {
        private readonly AppDbContext _context;

        public HocPhanController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("TenDangNhap")))
                return RedirectToAction("Login", "SinhVien");

            var danhSachHocPhan = _context.HocPhans.ToList();
            return View(danhSachHocPhan);
        }

       [HttpPost]
public IActionResult DangKy(string maHP)
{
    var tenDangNhap = HttpContext.Session.GetString("TenDangNhap");
    if (string.IsNullOrEmpty(tenDangNhap))
        return RedirectToAction("Login", "SinhVien");

    var sv = _context.SinhViens.FirstOrDefault(s => s.MaSV == tenDangNhap);
    if (sv == null) return NotFound();

    var dk = new DangKy { NgayDK = DateTime.Now, MaSV = sv.MaSV };
    _context.DangKys.Add(dk);
    _context.SaveChanges();

    var ct = new ChiTietDangKy { MaDK = dk.MaDK, MaHP = maHP };
    _context.ChiTietDangKys.Add(ct);
    _context.SaveChanges();

    return RedirectToAction("Index");
}

    }
}
