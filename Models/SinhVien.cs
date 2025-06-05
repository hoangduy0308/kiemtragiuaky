using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMvcApp.Models
{
    [Table("SinhVien")]
    public class SinhVien
    {
        [Key]
        public string? MaSV { get; set; }

        [Required]
        public string? HoTen { get; set; }

        public string? GioiTinh { get; set; }

        [DataType(DataType.Date)]
        public DateTime NgaySinh { get; set; }

        public string? Hinh { get; set; }

        public string? MaNganh { get; set; }
    }
}
