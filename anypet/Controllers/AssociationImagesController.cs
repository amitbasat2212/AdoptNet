using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdoptNet.Data;
using anypet.Models;
using Microsoft.AspNetCore.Authorization;

namespace AdoptNet.Controllers
{
    public class AssociationImagesController : Controller
    {
        private readonly AdoptNetContext _context;
        public AssociationImagesController(AdoptNetContext context)
        {
            _context = context;
        }
        // GET: AssociationImages
        public async Task<IActionResult> Index()
        {
            var adoptNetContext = _context.AssociationImage.Include(a => a.Association);
            return View(await adoptNetContext.ToListAsync());
        }
        // GET: AssociationImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var associationImage = await _context.AssociationImage
                .Include(a => a.Association)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (associationImage == null)
            {
                return NotFound();
            }
            return View(associationImage);
        }

        // GET: AssociationImages/Create
       // [Authorize(Roles = "Admin, Association")]
        public IActionResult Create()
        {
            ViewData["AssociationId"] = new SelectList(_context.Association, "Id", nameof(Association.Name));
            return View();
        }
        // POST: AssociationImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Image,AssociationId")] AssociationImage associationImage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(associationImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssociationId"] = new SelectList(_context.Association, "Id", nameof(Association.Name), associationImage.AssociationId);
            return View(associationImage);
        }

        // GET: AssociationImages/Edit/5
       // [Authorize(Roles = "Admin, Association")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var associationImage = await _context.AssociationImage.FindAsync(id);
            if (associationImage == null)
            {
                return NotFound();
            }
            ViewData["AssociationId"] = new SelectList(_context.Association, "Id", nameof(Association.Name), associationImage.AssociationId);
            return View(associationImage);
        }
        // POST: AssociationImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image,AssociationId")] AssociationImage associationImage)
        {
            if (id != associationImage.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(associationImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssociationImageExists(associationImage.Id))
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
            ViewData["AssociationId"] = new SelectList(_context.Association, "Id", nameof(Association.Name), associationImage.AssociationId);
            return View(associationImage);
        }

        // GET: AssociationImages/Delete/5
       // [Authorize(Roles = "Admin, Association")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var associationImage = await _context.AssociationImage
                .Include(a => a.Association)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (associationImage == null)
            {
                return NotFound();
            }
            return View(associationImage);
        }
        // POST: AssociationImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var associationImage = await _context.AssociationImage.FindAsync(id);
            _context.AssociationImage.Remove(associationImage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool AssociationImageExists(int id)
        {
            return _context.AssociationImage.Any(e => e.Id == id);
        }
    }
}