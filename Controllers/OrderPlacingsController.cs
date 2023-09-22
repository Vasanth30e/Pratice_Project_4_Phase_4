using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaApplication.Models;

namespace PizzaApplication.Controllers
{
    public class OrderPlacingsController : Controller
    {
        private readonly PizzaDbContext _context;

        public OrderPlacingsController(PizzaDbContext context)
        {
            _context = context;
        }

        // GET: OrderPlacings
        public async Task<IActionResult> Index()
        {
              return _context.OrderPlacings != null ? 
                          View(await _context.OrderPlacings.ToListAsync()) :
                          Problem("Entity set 'PizzaDbContext.OrderPlacings'  is null.");
        }

        // GET: OrderPlacings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderPlacings == null)
            {
                return NotFound();
            }

            var orderPlacing = await _context.OrderPlacings
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (orderPlacing == null)
            {
                return NotFound();
            }

            return View(orderPlacing);
        }

        // GET: OrderPlacings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderPlacings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,CustomerName,OrderDate,ProductName,PhoneNumber,Quantity,TotalAmount")] OrderPlacing orderPlacing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderPlacing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderPlacing);
        }

        // GET: OrderPlacings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrderPlacings == null)
            {
                return NotFound();
            }

            var orderPlacing = await _context.OrderPlacings.FindAsync(id);
            if (orderPlacing == null)
            {
                return NotFound();
            }
            return View(orderPlacing);
        }

        // POST: OrderPlacings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,CustomerName,OrderDate,ProductName,PhoneNumber,Quantity,TotalAmount")] OrderPlacing orderPlacing)
        {
            if (id != orderPlacing.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderPlacing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderPlacingExists(orderPlacing.CustomerId))
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
            return View(orderPlacing);
        }

        // GET: OrderPlacings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderPlacings == null)
            {
                return NotFound();
            }

            var orderPlacing = await _context.OrderPlacings
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (orderPlacing == null)
            {
                return NotFound();
            }

            return View(orderPlacing);
        }

        // POST: OrderPlacings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderPlacings == null)
            {
                return Problem("Entity set 'PizzaDbContext.OrderPlacings'  is null.");
            }
            var orderPlacing = await _context.OrderPlacings.FindAsync(id);
            if (orderPlacing != null)
            {
                _context.OrderPlacings.Remove(orderPlacing);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderPlacingExists(int id)
        {
          return (_context.OrderPlacings?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        }
    }
}
