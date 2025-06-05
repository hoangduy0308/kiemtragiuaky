using System.ComponentModel.DataAnnotations.Schema;

namespace MyMvcApp.Models
{
    [Table("ChiTietDangKy")]
    public class ChiTietDangKy
    {
        public int MaDK { get; set; }
        public string? MaHP { get; set; }

        [ForeignKey("MaDK")]
        public DangKy? DangKy { get; set; }

        [ForeignKey("MaHP")]
        public HocPhan? HocPhan { get; set; }
    }
}
