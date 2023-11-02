using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUD.Models;

namespace CRUD.Controllers
{
    public class CustomerDetailsController : Controller
    {
        private readonly CustomerDbContext _context;

        public CustomerDetailsController(CustomerDbContext context)
        {
            _context = context;
        }

        // GET: CustomerDetails
        public async Task<IActionResult> Index()
        {
              return _context.CustomerDetails != null ? 
                          View(await _context.CustomerDetails.ToListAsync()) :
                          Problem("Entity set 'CustomerDbContext.CustomerDetails'  is null.");
        }

        // GET: CustomerDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CustomerDetails == null)
            {
                return NotFound();
            }

            var customerDetails = await _context.CustomerDetails
                .FirstOrDefaultAsync(m => m.CustId == id);
            if (customerDetails == null)
            {
                return NotFound();
            }

            return View(customerDetails);
        }

        // GET: CustomerDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomerDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustId,CustName,CustAdd,CustEmail,CustMobNo")] CustomerDetails customerDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerDetails);
        }

        // GET: CustomerDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CustomerDetails == null)
            {
                return NotFound();
            }

            var customerDetails = await _context.CustomerDetails.FindAsync(id);
            if (customerDetails == null)
            {
                return NotFound();
            }
            return View(customerDetails);
        }

        // POST: CustomerDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustId,CustName,CustAdd,CustEmail,CustMobNo")] CustomerDetails customerDetails)
        {
            if (id != customerDetails.CustId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerDetailsExists(customerDetails.CustId))
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
            return View(customerDetails);
        }

        // GET: CustomerDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CustomerDetails == null)
            {
                return NotFound();
            }

            var customerDetails = await _context.CustomerDetails
                .FirstOrDefaultAsync(m => m.CustId == id);
            if (customerDetails == null)
            {
                return NotFound();
            }

            return View(customerDetails);
        }

        // POST: CustomerDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CustomerDetails == null)
            {
                return Problem("Entity set 'CustomerDbContext.CustomerDetails'  is null.");
            }
            var customerDetails = await _context.CustomerDetails.FindAsync(id);
            if (customerDetails != null)
            {
                _context.CustomerDetails.Remove(customerDetails);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerDetailsExists(int id)
        {
          return (_context.CustomerDetails?.Any(e => e.CustId == id)).GetValueOrDefault();
        }
    }
}
