
using System.ComponentModel.DataAnnotations;

namespace qlbb2.Data.Entities
{
    public class TblSupplier
    {
        [Key]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Tên nhà cung cấp không được để trống")]
        [StringLength(100, ErrorMessage = "Tên quá dài")]
        public string SupplierName { get; set; }

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Phone { get; set; }
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
