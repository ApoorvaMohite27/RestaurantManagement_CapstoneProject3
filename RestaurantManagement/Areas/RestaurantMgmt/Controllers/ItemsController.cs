using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantManagement.Data;
using RestaurantManagement.Models;

namespace RestaurantManagement.Areas.RestaurantMgmt.Controllers
{
    [Area("RestaurantMgmt")]
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ItemsController> _logger;

        public ItemsController(ApplicationDbContext context, ILogger<ItemsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: RestaurantMgmt/Items
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Items.Include(i => i.Category);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: LibMgmt/GetBooksOfCategory?filterCategoryId=5
        public async Task<IActionResult> GetItemsOfCategory(int filterCategoryId)
        {
            var viewmodel = await _context.Items
                                          .Where(b => b.CategoryId == filterCategoryId)
                                          .Include(b => b.Category)
                                          .ToListAsync();

            return View(viewName: "Index", model: viewmodel);
        }


        // GET: LibMgmt/GetBooksOfCategory?filterCategoryId=5
        public async Task<IActionResult> GetMenuOfItemCategory(int filterItemId)
        {
            var viewmodel = await _context.Items
                                          .Where(i => i.ItemId == filterItemId)
                                          .Include(b => b.Category)
                                          .ToListAsync();

            return View(viewName: "IndexCustomized", model: viewmodel);
        }

        public async Task<IActionResult> GetMenuCard()
        {
            var applicationDbContext = await _context.Items.Include(i => i.Category).ToListAsync();
            return View(viewName:"IndexCustomizedItem", model:applicationDbContext);
        }
        public async Task<IActionResult> GetMenuCardByPriceOrder()
        {
            var applicationDbContext = await _context.Items.OrderBy(i => i.ItemPrice)
                                                        .Include(i => i.Category).ToListAsync();
            return View(viewName: "IndexCustomizedItem", model: applicationDbContext);
        }

        // GET: RestaurantMgmt/Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: RestaurantMgmt/Items/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: RestaurantMgmt/Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,ItemName,ItemPrice,CategoryId")] Item item)
        {
            // Sanitize the data
            item.ItemName = item.ItemName.Trim();

            // Validation Checks - Server-side validation
            bool duplicateExists = _context.Items.Any(c => c.ItemName == item.ItemName);
            if (duplicateExists)
            {
                ModelState.AddModelError("ItemName", "Duplicate Item Found!");
            }
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", item.CategoryId);
            return View(item);
        }

        // GET: RestaurantMgmt/Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", item.CategoryId);
            return View(item);
        }

        // POST: RestaurantMgmt/Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,ItemName,ItemPrice,CategoryId")] Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            // Sanitize the data
            item.ItemName = item.ItemName.Trim();

            // Validation Checks - Server-side validation
            bool duplicateExists = _context.Items
                .Any(c => c.ItemName == item.ItemName && c.ItemId != item.ItemId);
            if (duplicateExists)
            {
                ModelState.AddModelError("ItemName", "Duplicate Item Found!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", item.CategoryId);
            return View(item);
        }

        // GET: RestaurantMgmt/Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: RestaurantMgmt/Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }
    }
}
