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
using AdoptNet.Models;

namespace AdoptNet.Controllers
{
    public class AssociationsController : Controller
    {
        private readonly AdoptNetContext _context;
        public AssociationsController(AdoptNetContext context)
        {
            _context = context;
        }

       
        // GET: Associations
        public async Task<IActionResult> Index()
        {
            var adoptNetContext = _context.Association.Include(a => a.AssociationImage).Include(a=>a.AdoptionDays);
            return View(await adoptNetContext.ToListAsync());
        }
        // GET: Associations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var association = await _context.Association
                .FirstOrDefaultAsync(m => m.Id == id);
            var adoptionday = await _context.AdoptionDays
               .FirstOrDefaultAsync(m => m.Id == id);
            if (association == null)
            {
                return NotFound();
            }
            return View(association);
        }

        // GET: Associations/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["Adoption"] = new SelectList(_context.AdoptionDays, "Id", "Name");
            return View();
        }
        // POST: Associations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PhoneNumber,Location,EmailOfUser")] Association association, int[] AdoptionDays )
        {
            if (ModelState.IsValid)
            {
                association.AdoptionDays = new List<AdoptionDays>();
                association.AdoptionDays.AddRange(_context.AdoptionDays.Where(x => AdoptionDays.Contains(x.Id)));
                
                _context.Association.Add(association);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Adoption"] = new SelectList(_context.AdoptionDays, "Id", "Name");
            return View(association);
        }

        // GET: Associations/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var association = await _context.Association.FindAsync(id);
            if (association == null)
            {
                return NotFound();
            }
            ViewData["Adoption"] = new SelectList(_context.AdoptionDays, "Id", "Name") ;
            return View(association);
        }
        // POST: Associations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PhoneNumber,Location,EmailOfUser")] Association association, int[] AdoptionDays)
        {
            if (id != association.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    
                    Association associationtobeupdate = await _context.Association.Include(p => p.AdoptionDays).FirstOrDefaultAsync(p => p.Id == id);
                    if (associationtobeupdate != null)
                    {
                        foreach (var scId in association.AdoptionDays)
                        {
                            associationtobeupdate.AdoptionDays.Add(new AdoptionDays()
                            {
                                Id = associationtobeupdate.Id
                            });
                        }

                    }

                               
                                             
                        _context.Association.Update(association);
                         await _context.SaveChangesAsync();


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssociationExists(association.Id))
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
            ViewData["Adoption"] = new SelectList(_context.AdoptionDays, "Id", "Name");
            return View(association);
        }

        // GET: Associations/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var association = await _context.Association
                .FirstOrDefaultAsync(m => m.Id == id);
            if (association == null)
            {
                return NotFound();
            }
            return View(association);
        }
        // POST: Associations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            var association = await _context.Association.FindAsync(id);
            _context.Association.Remove(association);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool AssociationExists(int id)
        {
            return _context.Association.Any(e => e.Id == id);
        }
       

    }
}