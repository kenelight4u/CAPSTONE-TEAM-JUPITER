using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Models
{
    public class SubCategory
    {

        [Key]
        public string Id { get; set; }
        [Required(ErrorMessage ="Subcategory name cannot be left blank")]
        [MaxLength(32, ErrorMessage ="Subcategory name cannot be longer than 32 characters")]
        public string SubCategoryName { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public DateTime LastModified { get; set; }
        public virtual Category Category { get; set; }
        public string CategoryId { get; set; }
        public virtual List<Product> Products { get; set; }

    }
}