using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RestaurantManagement.Models
{
    [Table("CustomerDetails")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Customer Id")]
        public int CustomerId { get; set; }

        [Display(Name = "Customer Name")]
        [Required(ErrorMessage = "{0} cannot be empty!")]
        [MaxLength(50, ErrorMessage = "{0} not more than {1} characters!")]
        [MinLength(5, ErrorMessage = "{0} not less than {1} characters!")]
        [RegularExpression("^[A-Za-z\\s]+$", ErrorMessage = "{0} not valid")]
        public string CustomerName { get; set; }

        [Display(Name = "Birth Date")]
        [Required(ErrorMessage = "{0} cannot be empty!")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required]
        public string Gender { get; set; }

        [Display(Name = "Email Id")]
        [DataType(DataType.EmailAddress, ErrorMessage = "{0} not valid!")]
        public string EmailId { get; set; }


        [Display(Name = "Mobile Number")]
        [RegularExpression("^[0-9]{10}", ErrorMessage = "{0} not valid!")]
        [Required(ErrorMessage = "{0} cannot be empty!")]
        public string MobileNumber { get; set; }



        #region

        [JsonIgnore]
        public ICollection<CustomerOrderDetail> CustomerOrderDetails { get; set; }

        #endregion
    }
}



        

        