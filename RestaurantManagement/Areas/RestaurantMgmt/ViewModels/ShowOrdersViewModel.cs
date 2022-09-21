using RestaurantManagement.Models;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RestaurantManagement.Areas.RestaurantMgmt.ViewModels
{
    public class ShowOrdersViewModel
    {
        [Display(Name = "Select Customer:")]
        [Required(ErrorMessage = "Please select a Customer for displaying the Order Details")]
        public int CustomerId { get; set; } 

        public ICollection<Customer> Customers { get; set; }
    }
}
