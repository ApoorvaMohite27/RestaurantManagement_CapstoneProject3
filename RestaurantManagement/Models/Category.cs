using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantManagement.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Category Id")]
        public int CategoryId { get; set; }

        [Display(Name = "Category Name")]
        [Required(ErrorMessage ="{0} cannot be empty!")]
        [StringLength(50, ErrorMessage = "{0} cannot have more than {1} characters.")]
        public string CategoryName { get; set; }

        #region Navigation Properties to the Item Class

        public ICollection<Item> Items { get; set; }

        #endregion

    }
}
