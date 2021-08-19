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

namespace anypet.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AdoptNetContext _context;

        public ProductsController(AdoptNetContext context)
        {
            _context = context;
        }

        // GET: Products
        [Authorize(Roles = "Admin,Association,Client")]
        public async Task<IActionResult> Index()
        {
            var adoptNetContext = (from As in _context.Products
                                   join im in _context.Animal
                                   on As.AnimalId equals im.Id
                                   select new Products
                                   {
                                       Id = As.Id,
                                       Food = As.Food,
                                       Medicine = As.Medicine,
                                       Toy = As.Toy,
                                       Animal = im,
                                       AnimalId = As.AnimalId
                                   }
                                   );


            //var adoptNetContext = _context.Products.Include(p => p.Animal);
            return View(await adoptNetContext.ToListAsync());
        }

       
        // GET: Products/Create
        [Authorize(Roles = "Admin,Association")]
        public IActionResult Create()
        {
            ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", nameof(Animal.Name));
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Food,Toy,Medicine,AnimalId")] Products products)
        {
            if (ModelState.IsValid)
            {
                var check = _context.Products.Where(r => r.AnimalId.Equals(products.AnimalId));
                if (check.Count() == 0)
                {
                    _context.Add(products);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewData["Error"] = "this Animal allready  have an Product ";
                }
            }
            ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", nameof(Animal.Name), products.AnimalId);
            return View(products);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin,Association")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }
            ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", nameof(Animal.Name), products.AnimalId);
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Food,Toy,Medicine,AnimalId")] Products products)
        {
            if (id != products.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(products.Id))
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
            ViewData["AnimalId"] = new SelectList(_context.Animal, "Id", nameof(Animal.Name), products.AnimalId);
            return View(products);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin,Association")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.Animal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _context.Products.FindAsync(id);
            _context.Products.Remove(products);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
