using System.ComponentModel.DataAnnotations;

namespace qlbb2.Entities
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }
        [Required]
        public string SupplierName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
