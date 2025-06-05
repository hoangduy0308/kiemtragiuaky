using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMvcApp.Models
{
    [Table("NganhHoc")]
    public class NganhHoc
    {
        [Key]
        public string MaNganh { get; set; }

        public string? TenNganh { get; set; }
    }
}
