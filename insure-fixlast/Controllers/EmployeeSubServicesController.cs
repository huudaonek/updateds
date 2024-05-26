using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using insure_fixlast.Data;
using insure_fixlast.Models;

namespace insure_fixlast.Controllers
{
    public class EmployeeSubServicesController : Controller
    {
        private readonly insure_fixlastContext _context;

        public EmployeeSubServicesController(insure_fixlastContext context)
        {
            _context = context;
        }

        // GET: EmployeeSubServices
        public async Task<IActionResult> Index()
        {
            var insure_fixlastContext = _context.EmployeeSubService.Include(e => e.Employee);
            return View(await insure_fixlastContext.ToListAsync());
        }

        // GET: EmployeeSubServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeSubService = await _context.EmployeeSubService
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeSubService == null)
            {
                return NotFound();
            }

            return View(employeeSubService);
        }

        // GET: EmployeeSubServices/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Id");
            return View();
        }

        // POST: EmployeeSubServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId")] EmployeeSubService employeeSubService)
        {
            if (true)
            {
                _context.Add(employeeSubService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Id", employeeSubService.EmployeeId);
            return View(employeeSubService);
        }

        // GET: EmployeeSubServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeSubService = await _context.EmployeeSubService.FindAsync(id);
            if (employeeSubService == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Id", employeeSubService.EmployeeId);
            return View(employeeSubService);
        }

        // POST: EmployeeSubServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId")] EmployeeSubService employeeSubService)
        {
            if (id != employeeSubService.Id)
            {
                return NotFound();
            }

            if (true)
            {
                try
                {
                    _context.Update(employeeSubService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeSubServiceExists(employeeSubService.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Id", employeeSubService.EmployeeId);
            return View(employeeSubService);
        }

        // GET: EmployeeSubServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeSubService = await _context.EmployeeSubService
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeSubService == null)
            {
                return NotFound();
            }

            return View(employeeSubService);
        }

        // POST: EmployeeSubServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeSubService = await _context.EmployeeSubService.FindAsync(id);
            if (employeeSubService != null)
            {
                _context.EmployeeSubService.Remove(employeeSubService);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeSubServiceExists(int id)
        {
            return _context.EmployeeSubService.Any(e => e.Id == id);
        }
    }
}
