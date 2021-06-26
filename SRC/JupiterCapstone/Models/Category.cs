using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.Models
{
    public class Category
    {
        public Category()
        {
            SubCategories = new HashSet<SubCategory>();

        }
        [Key]
        public string Id { get; set; }
        [Required(ErrorMessage ="Category Name cannot be left blank")]
        [MaxLength(32, ErrorMessage ="Category Name cannot be longer than 32 characters")]
        public string CategoryName { get; set; }
        public bool IsDeleted{ get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public DateTime LastModified { get; set; }
        public virtual IEnumerable<SubCategory> SubCategories { get; set; }
    }
}
   


