using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Data;
using RestaurantManagement.Models;
using RestaurantManagement.Areas.Demo.Controllers;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;

namespace RestaurantManagement.Areas.RestaurantMgmt.Controllers
{
    [Area("RestaurantMgmt")]
    public class CustomerOrderDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerOrderDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RestaurantMgmt/CustomerOrderDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CustomerOrderDetails.Include(c => c.Customer).Include(c => c.Item);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> DisplayForAdmin()
        {
            var applicationDbContext = await _context.CustomerOrderDetails.Include(c => c.Customer).Include(c => c.Item).ToListAsync();
            return View(viewName: "IndexCopy", model:applicationDbContext);
        }




        public async Task<IActionResult> GetOrdersOfCustomer(int filterCustomerId)
        {
            var viewmodel = await _context.CustomerOrderDetails.Where(c => c.CustomerId == filterCustomerId)
                                            .Include(c => c.Customer).Include(c => c.Item).ToListAsync();
            return View(viewName:"Index" ,  model:viewmodel);
        }





        // GET: RestaurantMgmt/CustomerOrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerOrderDetail = await _context.CustomerOrderDetails
                .Include(c => c.Customer)
                .Include(c => c.Item)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (customerOrderDetail == null)
            {
                return NotFound();
            }

            return View(customerOrderDetail);
        }

        //// GET: RestaurantMgmt/CustomerOrderDetails/Create
        //public IActionResult Create()
        //{
        //    CustomerOrderDetail customerOrderDetail = new CustomerOrderDetail();
        //    ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerName");



        //    ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemName");
        //    return View(customerOrderDetail);
        //}


        // GET: RestaurantMgmt/CustomerOrderDetails/Create
        public IActionResult Create(int id)
        {
            CustomerOrderDetail customerOrderDetail = new CustomerOrderDetail();
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerName");

            var query = _context.Items.Where(i => i.ItemId == id);
            ViewData["ItemId"] = new SelectList(query, "ItemId", "ItemName");
            //ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemName");
            return View(customerOrderDetail);
        }

        // POST: RestaurantMgmt/CustomerOrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,Amount,TotalCost,OrderDateTime,IsEnabled,ItemId,CustomerId")] CustomerOrderDetail customerOrderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerOrderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(actionName: "Menu",
                                        controllerName: "Home",
                                        routeValues: new { area = "Demo" });
                
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerName", customerOrderDetail.CustomerId);
            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemName", customerOrderDetail.ItemId);
            return View(customerOrderDetail);
        }

        // GET: RestaurantMgmt/CustomerOrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerOrderDetail = await _context.CustomerOrderDetails.FindAsync(id);
            if (customerOrderDetail == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerName", customerOrderDetail.CustomerId);
            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemName", customerOrderDetail.ItemId);
            return View(customerOrderDetail);
        }

        // POST: RestaurantMgmt/CustomerOrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,Amount,TotalCost,OrderDateTime,IsEnabled,ItemId,CustomerId")] CustomerOrderDetail customerOrderDetail)
        {
            if (id != customerOrderDetail.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerOrderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerOrderDetailExists(customerOrderDetail.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(actionName: "Index",
                                        controllerName: "ShowOrders",
                                        routeValues: new { area = "RestaurantMgmt" });
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerName", customerOrderDetail.CustomerId);
            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemName", customerOrderDetail.ItemId);
            return View(customerOrderDetail);
        }

        // GET: RestaurantMgmt/CustomerOrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerOrderDetail = await _context.CustomerOrderDetails
                .Include(c => c.Customer)
                .Include(c => c.Item)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (customerOrderDetail == null)
            {
                return NotFound();
            }

            return View(customerOrderDetail);
        }

        // POST: RestaurantMgmt/CustomerOrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerOrderDetail = await _context.CustomerOrderDetails.FindAsync(id);
            _context.CustomerOrderDetails.Remove(customerOrderDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerOrderDetailExists(int id)
        {
            return _context.CustomerOrderDetails.Any(e => e.OrderId == id);
        }
    }
}
