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
        // The connection between many to many -> adoption days and associations
        // one to one -> each association has it own image
        [Authorize(Roles = "Admin,Association,Client")]
        public async Task<IActionResult> Index()
        {
            var adoptNetContext = _context.Association.Include(a => a.AssociationImage).Include(a => a.AdoptionDays);
            return View(await adoptNetContext.ToListAsync());
        }
        // GET: Associations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var association = await _context.Association.Include(a => a.AdoptionDays)
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

        // many to many - creating new association and allocating memory to adoption days to each association
        // this connecting is also shows in the adoption days
        // POST: Associations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PhoneNumber,Location,EmailOfUser")] Association association, int[] AdoptionDays)
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



        public async Task<IActionResult> Search(String Searching)
        {  // Use LINQ to get list of genres.

            var adoptionDays = _context.Association.Include(a => a.Animals).Include(b => b.AssociationImage).Include(c => c.AdoptionDays).Where(b => (b.Name.Contains(Searching) || Searching == null) || (b.Location.Contains(Searching) || Searching == null));
            return View("Index", await adoptionDays.ToListAsync());
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
            ViewData["Adoption"] = new SelectList(_context.AdoptionDays, "Id", "Name");

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
                    Association adoptNetContext = (Association)_context.Association.Include(a => a.AdoptionDays).Where(r => r.Id == id).FirstOrDefault();
                    adoptNetContext.Animals = association.Animals;
                    adoptNetContext.AssociationImage = association.AssociationImage;
                    adoptNetContext.EmailOfUser = association.EmailOfUser;
                    adoptNetContext.Location = association.Location;
                    adoptNetContext.Name = association.Name;
                    adoptNetContext.PhoneNumber = association.PhoneNumber;

                    if (AdoptionDays.Length > 0)
                    {
                        for (int i = 0; i < AdoptionDays.Length; i++)
                        {
                            AdoptionDays adopt = _context.AdoptionDays.Single(n => n.Id == AdoptionDays[i]);

                            if (adoptNetContext.AdoptionDays.Contains(adopt))
                            {
                                ViewData["Error"] = "this Association allready  have this Adoption day  ";
                                ViewData["Adoption"] = new SelectList(_context.AdoptionDays, "Id", "Name");
                                return View(adoptNetContext);

                            }
                            else
                            {
                                adoptNetContext.AdoptionDays.Add(adopt);
                                _context.Update(adoptNetContext);
                                await _context.SaveChangesAsync();
                            }


                        }
                    }
                    else if (AdoptionDays.Length == 0)
                    {
                        _context.Update(adoptNetContext);
                        await _context.SaveChangesAsync();

                    }

                    else
                    {
                        adoptNetContext.AdoptionDays = new List<AdoptionDays>();
                        adoptNetContext.AdoptionDays.AddRange(_context.AdoptionDays.Where(x => AdoptionDays.Contains(x.Id)));
                        _context.Update(adoptNetContext);
                        await _context.SaveChangesAsync();
                    }
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

        public JsonResult GetAssociationPlace()
        {
            List<Association> AssociationList = new List<Association>();
            foreach (var item in _context.Association)
            {
                AssociationList.Add(item);

            }
            return Json(AssociationList);
        }

        //graph1
        public JsonResult GetAnimalsCount()
        {
            List<String> Res = new List<String>();
            int AmountOfDogs = _context.Animal.Where(a => a.Kind == Kind.Dog).Count();
            int AmountOfCats = _context.Animal.Where(a => a.Kind == Kind.Cat).Count();

            Res.Add(AmountOfCats.ToString());
            Res.Add(AmountOfDogs.ToString());

            return Json(Res);
        }

    }
}