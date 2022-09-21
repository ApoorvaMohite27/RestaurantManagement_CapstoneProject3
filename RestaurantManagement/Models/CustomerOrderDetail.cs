using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RestaurantManagement.Models
{
    [Table("CustomerOrderDetails")]
    public class CustomerOrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Order Id")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "{0} cannot be empty!")]
        public int Amount { get; set; }

        [Display(Name ="Total Cost")]
        [DataType(DataType.Currency)]
        public decimal TotalCost { get; set; }
        

        public DateTime OrderDateTime { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "*")]
        [Display(Name ="Confirm Order")]
        public bool IsEnabled { get; set; }

        #region Navigation Properties to the Item Model

        [Required]
        public int ItemId { get; set; }

        [ForeignKey(nameof(CustomerOrderDetail.ItemId))]
        public Item Item { get; set; }

        #endregion



        #region Navigation Properties to the Customer Model

        [Required]
        public int CustomerId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(CustomerOrderDetail.CustomerId))]
        public Customer Customer { get; set; }

        #endregion
    }
}
