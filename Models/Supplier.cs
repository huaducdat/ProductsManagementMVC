
using System.ComponentModel.DataAnnotations;

namespace ProductSupplierMVC.Models
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên nhà cung cấp")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "Tên phải từ 3 đến 100 ký tự")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [StringLength(15,
            ErrorMessage = "Số điện thoại tối đa 15 ký tự")]
        public string Phone { get; set; } = string.Empty;

        public string? Address { get; set; }

        // Một Supplier có nhiều Products
        public ICollection<Product> Products { get; set; }
            = new List<Product>();
    }
}
