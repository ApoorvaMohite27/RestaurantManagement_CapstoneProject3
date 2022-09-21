using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration;
using RestaurantManagement.Areas.RestaurantMgmt.ViewModels;
using RestaurantManagement.Data;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantManagement.Areas.RestaurantMgmt.Controllers
{
    [Area("RestaurantMgmt")]
    public class ShowOrdersController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ShowOrdersController> _logger;

        public ShowOrdersController(ApplicationDbContext dbContext, ILogger<ShowOrdersController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            PopulateDropDownListToSelectCustomer();

            _logger.LogInformation("--- extracted Customer");
            return View();
        }

        private void PopulateDropDownListToSelectCustomer()
        {
            List<SelectListItem> customer = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "--- select a customer ---",
                    Value = "",
                    Selected = true
                }
            };
            customer.AddRange(new SelectList(_dbContext.Customer, "CustomerId", "CustomerName"));

            ViewData["CustomersCollection"] = customer;
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("CustomerId")]ShowOrdersViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropDownListToSelectCustomer();

                return View(viewModel);
            }
            bool orderExist = _dbContext.CustomerOrderDetails.Any(c => c.CustomerId == viewModel.CustomerId);
            if (!orderExist)
            {
                ModelState.AddModelError("", "No Order were found for the selected customer!");
                PopulateDropDownListToSelectCustomer();
                return View(viewModel);
            }

            return RedirectToAction(actionName: "GetOrdersOfCustomer",
                controllerName: "CustomerOrderDetails",
                routeValues: new { area = "RestaurantMgmt", filterCustomerId = viewModel.CustomerId });
        }
    }
}
