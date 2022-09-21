using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RestaurantManagement.Models
{
    [Table("Items")]
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Item Id")]
        public int ItemId { get; set; }

        [Required(ErrorMessage ="{0} cannot be empty!")]
        [Display(Name ="Item Name")]
        [StringLength(50)]
        public string ItemName { get; set; }

        [Required]
        [Display(Name ="Item Price")]
        [DataType(DataType.Currency)]
        public decimal ItemPrice { get; set; }

        #region Navigation Properties to the Category class

        [Required]
        public int CategoryId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(Item.CategoryId))]
        public Category Category { get; set; }

        #endregion



        #region Navigation Properties to the CustomerOrderDetails Class

        [JsonIgnore]
        public ICollection<CustomerOrderDetail> CustomerOrderDetails { get; set; }

        #endregion
    }
}
