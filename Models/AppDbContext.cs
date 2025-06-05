using Microsoft.EntityFrameworkCore;
using MyMvcApp.Models;

public class AppDbContext : DbContext
{
    public DbSet<SinhVien> SinhViens { get; set; }
    public DbSet<HocPhan> HocPhans { get; set; }
    public DbSet<DangKy> DangKys { get; set; }
    public DbSet<ChiTietDangKy> ChiTietDangKys { get; set; }

    // ✅ Bổ sung để fix lỗi "Cannot create DbSet for NganhHoc"
    public DbSet<NganhHoc> NganhHocs { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ✅ Khóa chính cho ChiTietDangKy là khóa tổng hợp
        modelBuilder.Entity<ChiTietDangKy>()
            .HasKey(c => new { c.MaDK, c.MaHP });

        modelBuilder.Entity<ChiTietDangKy>()
            .HasOne(c => c.DangKy)
            .WithMany(d => d.ChiTietDangKys)
            .HasForeignKey(c => c.MaDK);

        modelBuilder.Entity<ChiTietDangKy>()
            .HasOne(c => c.HocPhan)
            .WithMany(h => h.ChiTietDangKys)
            .HasForeignKey(c => c.MaHP);
    }
}
