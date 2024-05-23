using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NNTracker.Models
{

    public class Category
    {
        [Key]
        public int Category_Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s_]{3,}$", ErrorMessage = "please enter at least 3 letters")]
        public string Category_Name { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[1-9])\d*\.?\d+$", ErrorMessage = "please enter at least one digit")]
        public double budget { get; set; }
        [Required]
        public string Type { get; set; }
        public string? Icon { get; set; }

        [NotMapped]
        public string? TitleWithIcon
        {
            get
            {
                return this.Icon + " " + this.Category_Name;
            }
        }
    }
}
