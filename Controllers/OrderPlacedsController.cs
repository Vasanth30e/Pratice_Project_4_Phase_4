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
    public class OrderPlacedsController : Controller
    {
        private readonly PizzaDbContext _context;

        public OrderPlacedsController(PizzaDbContext context)
        {
            _context = context;
        }

        // GET: OrderPlaceds
        public async Task<IActionResult> Index()
        {
              return _context.OrderPlaceds != null ? 
                          View(await _context.OrderPlaceds.ToListAsync()) :
                          Problem("Entity set 'PizzaDbContext.OrderPlaceds'  is null.");
        }

        // GET: OrderPlaceds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderPlaceds == null)
            {
                return NotFound();
            }

            var orderPlaced = await _context.OrderPlaceds
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderPlaced == null)
            {
                return NotFound();
            }

            return View(orderPlaced);
        }

        // GET: OrderPlaceds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderPlaceds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,CustomerName,ProductName,Quantity,TotalAmount")] OrderPlaced orderPlaced)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderPlaced);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderPlaced);
        }

        // GET: OrderPlaceds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrderPlaceds == null)
            {
                return NotFound();
            }

            var orderPlaced = await _context.OrderPlaceds.FindAsync(id);
            if (orderPlaced == null)
            {
                return NotFound();
            }
            return View(orderPlaced);
        }

        // POST: OrderPlaceds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,CustomerName,ProductName,Quantity,TotalAmount")] OrderPlaced orderPlaced)
        {
            if (id != orderPlaced.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderPlaced);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderPlacedExists(orderPlaced.OrderId))
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
            return View(orderPlaced);
        }

        // GET: OrderPlaceds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderPlaceds == null)
            {
                return NotFound();
            }

            var orderPlaced = await _context.OrderPlaceds
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderPlaced == null)
            {
                return NotFound();
            }

            return View(orderPlaced);
        }

        // POST: OrderPlaceds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderPlaceds == null)
            {
                return Problem("Entity set 'PizzaDbContext.OrderPlaceds'  is null.");
            }
            var orderPlaced = await _context.OrderPlaceds.FindAsync(id);
            if (orderPlaced != null)
            {
                _context.OrderPlaceds.Remove(orderPlaced);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderPlacedExists(int id)
        {
          return (_context.OrderPlaceds?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
