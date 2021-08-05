using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdoptNet.Data;
using anypet.Models;

namespace anypet.Controllers
{
    public class AssociationImagesController : Controller
    {
        private readonly AdoptNetContext _context;

        public AssociationImagesController(AdoptNetContext context)
        {
            _context = context;
        }

      
       

        // GET: AssociationImages/Create
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
        public async Task<IActionResult> Create([Bind("Id,Image,AssociationId")] AssociationImages associationImages)
        {
            if (ModelState.IsValid)
            {
                var check = _context.AssociationImages.Where(r => r.AssociationId.Equals(associationImages.AssociationId));

                if (check.Count()==0)
                {
                    _context.Add(associationImages);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), "Associations");
                }
                else
                {
                    ViewData["Error"] = "this Association allready  have an image";
                }
            }
            ViewData["AssociationId"] = new SelectList(_context.Association, "Id", nameof(Association.Name));
            return View(associationImages);
        }

        // GET: AssociationImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var associationImages = await _context.AssociationImages.FindAsync(id);
            if (associationImages == null)
            {
                return NotFound();
            }
            ViewData["AssociationId"] = new SelectList(_context.Association, "Id", nameof(Association.Name), associationImages.AssociationId);
            return View(associationImages);
        }

        // POST: AssociationImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image,AssociationId")] AssociationImages associationImages)
        {
            if (id != associationImages.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(associationImages);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssociationImagesExists(associationImages.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), "Associations");
            }
            ViewData["AssociationId"] = new SelectList(_context.Association, "Id", nameof(Association.Name), associationImages.AssociationId);
            return View(associationImages);
        }

        // GET: AssociationImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var associationImages = await _context.AssociationImages
                .Include(a => a.Association)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (associationImages == null)
            {
                return NotFound();
            }

            return View(associationImages);
        }

        // POST: AssociationImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var associationImages = await _context.AssociationImages.FindAsync(id);
            _context.AssociationImages.Remove(associationImages);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), "Associations"); 
        }

        private bool AssociationImagesExists(int id)
        {
            return _context.AssociationImages.Any(e => e.Id == id);
        }
    }
}
