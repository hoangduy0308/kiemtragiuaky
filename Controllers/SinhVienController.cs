using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyMvcApp.Controllers
{
    public class SinhVienController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SinhVienController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("TenDangNhap")))
                return RedirectToAction("Login");

            var list = _context.SinhViens.ToList();
            return View(list);
        }

        public IActionResult Create()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("TenDangNhap")))
                return RedirectToAction("Login");

            var dsNganh = _context.Set<NganhHoc>().ToList();
            ViewBag.DanhSachNganh = new SelectList(dsNganh, "MaNganh", "TenNganh");
            return View();
        }

        [HttpPost]
        public IActionResult Create(SinhVien sv, IFormFile file)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("TenDangNhap")))
                return RedirectToAction("Login");

            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    sv.Hinh = uniqueFileName;
                }

                _context.SinhViens.Add(sv);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            var dsNganh = _context.Set<NganhHoc>().ToList();
            ViewBag.DanhSachNganh = new SelectList(dsNganh, "MaNganh", "TenNganh");
            return View(sv);
        }

        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("TenDangNhap")))
                return RedirectToAction("Login");

            var sv = _context.SinhViens.Find(id);
            if (sv == null)
                return NotFound();

            ViewBag.DanhSachNganh = new SelectList(_context.Set<NganhHoc>(), "MaNganh", "TenNganh", sv.MaNganh);
            return View(sv);
        }

        [HttpPost]
        public IActionResult Edit(SinhVien sv, IFormFile file)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("TenDangNhap")))
                return RedirectToAction("Login");

            if (ModelState.IsValid)
            {
                var svCu = _context.SinhViens.Find(sv.MaSV);
                if (svCu == null)
                    return NotFound();

                svCu.HoTen = sv.HoTen;
                svCu.GioiTinh = sv.GioiTinh;
                svCu.NgaySinh = sv.NgaySinh;
                svCu.MaNganh = sv.MaNganh;

                if (file != null && file.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    svCu.Hinh = uniqueFileName;
                }

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DanhSachNganh = new SelectList(_context.Set<NganhHoc>(), "MaNganh", "TenNganh", sv.MaNganh);
            return View(sv);
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("TenDangNhap")))
                return RedirectToAction("Login");

            var sv = _context.SinhViens.Find(id);
            if (sv != null)
            {
                _context.SinhViens.Remove(sv);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("TenDangNhap")))
                return RedirectToAction("Login");

            var sv = _context.SinhViens.Find(id);
            if (sv == null)
                return NotFound();

            var nganh = _context.Set<NganhHoc>().FirstOrDefault(n => n.MaNganh == sv.MaNganh);
            ViewBag.TenNganh = nganh?.TenNganh ?? "Không rõ";
            return View(sv);
        }

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
                return RedirectToAction("Index");
            }

            ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu sai!";
            return View();
        }
    }
}
