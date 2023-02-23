using System.ComponentModel.DataAnnotations;

namespace Nimap_Program.Models
{
    public class Category
    {
        public int CategoryID { get; set; }


        [Required(ErrorMessage = "Name field is empty")]
        public string? CategoryName { get; set; }

    }
}
