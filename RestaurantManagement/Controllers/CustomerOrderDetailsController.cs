using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Data;
using RestaurantManagement.Models;

namespace RestaurantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrderDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CustomerOrderDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CustomerOrderDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerOrderDetail>>> GetCustomerOrderDetails()
        {
            return await _context.CustomerOrderDetails.ToListAsync();
        }

        // GET: api/CustomerOrderDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerOrderDetail>> GetCustomerOrderDetail(int id)
        {
            var customerOrderDetail = await _context.CustomerOrderDetails.FindAsync(id);

            if (customerOrderDetail == null)
            {
                return NotFound();
            }

            return customerOrderDetail;
        }

        // PUT: api/CustomerOrderDetails/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerOrderDetail(int id, CustomerOrderDetail customerOrderDetail)
        {
            if (id != customerOrderDetail.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(customerOrderDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerOrderDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CustomerOrderDetails
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CustomerOrderDetail>> PostCustomerOrderDetail(CustomerOrderDetail customerOrderDetail)
        {
            _context.CustomerOrderDetails.Add(customerOrderDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerOrderDetail", new { id = customerOrderDetail.OrderId }, customerOrderDetail);
        }

        // DELETE: api/CustomerOrderDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerOrderDetail>> DeleteCustomerOrderDetail(int id)
        {
            var customerOrderDetail = await _context.CustomerOrderDetails.FindAsync(id);
            if (customerOrderDetail == null)
            {
                return NotFound();
            }

            _context.CustomerOrderDetails.Remove(customerOrderDetail);
            await _context.SaveChangesAsync();

            return customerOrderDetail;
        }

        private bool CustomerOrderDetailExists(int id)
        {
            return _context.CustomerOrderDetails.Any(e => e.OrderId == id);
        }
    }
}
