using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMvcApp.Models
{
    [Table("DangKy")]
    public class DangKy
    {
        [Key]
        public int MaDK { get; set; }

        public DateTime NgayDK { get; set; }

        public string? MaSV { get; set; }

        [ForeignKey("MaSV")]
        public SinhVien? SinhVien { get; set; }

        public ICollection<ChiTietDangKy>? ChiTietDangKys { get; set; } 
    }
}
