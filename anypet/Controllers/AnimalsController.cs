using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdoptNet.Data;
using AdoptNet.Models;
using Microsoft.AspNetCore.Authorization;

namespace AdoptNet.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly AdoptNetContext _context;
        public AnimalsController(AdoptNetContext context)
        {
            _context = context;
        }
        // GET: Animals
        public async Task<IActionResult> Index()
        {
            var adoptNetContext = _context.Animal.Include(a => a.Association);
            return View(await adoptNetContext.ToListAsync());
        }
        // GET: Animals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var animal = await _context.Animal
                .Include(a => a.Association)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return NotFound();
            }
            return View(animal);
        }

        // GET: Animals/Create
        [Authorize(Roles = "Admin, Association")]
        public IActionResult Create()
        {
            ViewData["AssociationId"] = new SelectList(_context.Association, "Id", nameof(Association.Name));
            return View();
        }
        // POST: Animals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Kind,Age,Gender,Description,Size,Location,AssociationId")] Animal animal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssociationId"] = new SelectList(_context.Association, "Id", nameof(Association.Name), animal.AssociationId);
            return View(animal);
        }

        // GET: Animals/Edit/5
        [Authorize(Roles = "Admin, Association")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var animal = await _context.Animal.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }
            ViewData["AssociationId"] = new SelectList(_context.Association, "Id", nameof(Association.Name), animal.AssociationId);
            return View(animal);
        }
        // POST: Animals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Kind,Age,Gender,Description,Size,Location,AssociationId")] Animal animal)
        {
            if (id != animal.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.Id))
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
            ViewData["AssociationId"] = new SelectList(_context.Association, "Id", nameof(Association.Name), animal.AssociationId);
            return View(animal);
        }

        // GET: Animals/Delete/5
        [Authorize(Roles = "Admin, Association")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var animal = await _context.Animal
                .Include(a => a.Association)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return NotFound();
            }
            return View(animal);
        }
        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animal = await _context.Animal.FindAsync(id);
            _context.Animal.Remove(animal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool AnimalExists(int id)
        {
            return _context.Animal.Any(e => e.Id == id);
        }
    }
}