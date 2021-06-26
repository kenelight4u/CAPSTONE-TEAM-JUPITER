using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Models
{
    public class Product
    {
        [Key]
        public string Id { get; set; }

        [Required(ErrorMessage ="Product Name cannot be left blank")]
        [MaxLength(32, ErrorMessage ="Product Name cannot be Longer than 32 Characters")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Product description cannot be left blank")]
        [MaxLength(256, ErrorMessage = "Product description cannot be Longer than 256 Characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Product quantity cannot be Left Blank")]
        public double Quantity { get; set; }

        [Required(ErrorMessage = "Price cannot be left blank")]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Range(0, 9999999999999999.99)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage ="Supplier name cannot be left blank")]
        [MaxLength(56, ErrorMessage = "Supplier Name cannot be Longer than 56 Characters")]
        public string SupplierName { get; set; }

        public string ImageUrl { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        public string Status { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public DateTime LastModified { get; set; }

        public virtual SubCategory SubCategory { get; set; } 
        public string SubCategoryId { get; set; }

    }
}
