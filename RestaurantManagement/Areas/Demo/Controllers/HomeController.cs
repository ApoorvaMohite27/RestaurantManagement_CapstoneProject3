using Microsoft.AspNetCore.Mvc;

namespace RestaurantManagement.Areas.Demo.Controllers
{
    [Area("Demo")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Menu()
        {
            return View();
        }

        public IActionResult DisplayListOfVegPizza()
        {
            return View();
        }

        public IActionResult DisplayListOfNonVegPizza()
        {
            return View();
        }

        public IActionResult DisplayListOfPizzaMania()
        {
            return View();
        }

        public IActionResult DisplayListOfSidesBeverages()
        {
            return View();
        }

        public IActionResult DisplayListOfPasta()
        {
            return View();
        }

       
    }
}
