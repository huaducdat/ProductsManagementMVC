
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductSupplierMVC.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        [StringLength(255, MinimumLength = 5,
            ErrorMessage = "Tên sản phẩm phải từ 5 đến 255 ký tự")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(typeof(decimal), "0", "9999999999999999.99",
            ErrorMessage = "Giá phải lớn hơn hoặc bằng 0")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue,
            ErrorMessage = "Số lượng không được âm")]
        public int Quantity { get; set; }

        public string? Category { get; set; }

        [Range(1, int.MaxValue,
            ErrorMessage = "Vui lòng chọn nhà cung cấp")]
        public int SupplierId { get; set; }

        // Navigation Property
        public Supplier? Supplier { get; set; }
    }
}
