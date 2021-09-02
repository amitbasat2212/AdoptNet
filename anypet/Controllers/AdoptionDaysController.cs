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
    public class AdoptionDaysController : Controller
    {
        private readonly AdoptNetContext _context;

        public AdoptionDaysController(AdoptNetContext context)
        {
            _context = context;
        }

        // GET: AdoptionDays
        [Authorize(Roles = "Admin,Association,Client")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.AdoptionDays.ToListAsync());
        }



        // GET: AdoptionDays/Create
        [Authorize(Roles = "Admin,Association")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdoptionDays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id,AdoptionDate,LocationAdopt,Description")] AdoptionDays adoptionDays)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adoptionDays);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adoptionDays);
        }




        public async Task<IActionResult> Search(String Searching)
        {  // Use LINQ to get list of genres.

            var adoptionDays = _context.AdoptionDays.Include(a => a.Associations).Where(b => (b.Name.Contains(Searching) || Searching == null) || (b.Description.Contains(Searching) || Searching == null));

            return View("Index", await adoptionDays.ToListAsync());
        }





        // GET: AdoptionDays/Edit/5
        [Authorize(Roles = "Admin,Association")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptionDays = await _context.AdoptionDays.FindAsync(id);
            if (adoptionDays == null)
            {
                return NotFound();
            }
            return View(adoptionDays);
        }

        // POST: AdoptionDays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Id,AdoptionDate,LocationAdopt,Description")] AdoptionDays adoptionDays)
        {
            if (id != adoptionDays.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adoptionDays);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdoptionDaysExists(adoptionDays.Id))
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
            return View(adoptionDays);
        }

        // GET: AdoptionDays/Delete/5
        [Authorize(Roles = "Admin,Association")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoptionDays = await _context.AdoptionDays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adoptionDays == null)
            {
                return NotFound();
            }

            return View(adoptionDays);
        }

        // POST: AdoptionDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adoptionDays = await _context.AdoptionDays.FindAsync(id);
            _context.AdoptionDays.Remove(adoptionDays);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdoptionDaysExists(int id)
        {
            return _context.AdoptionDays.Any(e => e.Id == id);
        }

        public JsonResult GetAdoptDayCount()
        {
            List<String> Res = new List<String>();

            int AmountOfJan = _context.AdoptionDays.Where(a => a.AdoptionDate.Month == 1).Count();
            int AmountOfFeb = _context.AdoptionDays.Where(a => a.AdoptionDate.Month == 2).Count();
            int AmountOfMar = _context.AdoptionDays.Where(a => a.AdoptionDate.Month == 3).Count();
            int AmountOfApr = _context.AdoptionDays.Where(a => a.AdoptionDate.Month == 4).Count();
            int AmountOfMay = _context.AdoptionDays.Where(a => a.AdoptionDate.Month == 5).Count();
            int AmountOfJun = _context.AdoptionDays.Where(a => a.AdoptionDate.Month == 6).Count();
            int AmountOfJul = _context.AdoptionDays.Where(a => a.AdoptionDate.Month == 7).Count();
            int AmountOfAug = _context.AdoptionDays.Where(a => a.AdoptionDate.Month == 8).Count();
            int AmountOfSep = _context.AdoptionDays.Where(a => a.AdoptionDate.Month == 9).Count();
            int AmountOfOct = _context.AdoptionDays.Where(a => a.AdoptionDate.Month == 10).Count();
            int AmountOfNov = _context.AdoptionDays.Where(a => a.AdoptionDate.Month == 11).Count();
            int AmountOfDec = _context.AdoptionDays.Where(a => a.AdoptionDate.Month == 12).Count();

            Res.Add(AmountOfJan.ToString());
            Res.Add(AmountOfFeb.ToString());
            Res.Add(AmountOfMar.ToString());
            Res.Add(AmountOfApr.ToString());
            Res.Add(AmountOfMay.ToString());
            Res.Add(AmountOfJun.ToString());
            Res.Add(AmountOfJul.ToString());
            Res.Add(AmountOfAug.ToString());
            Res.Add(AmountOfSep.ToString());
            Res.Add(AmountOfOct.ToString());
            Res.Add(AmountOfNov.ToString());
            Res.Add(AmountOfDec.ToString());

            return Json(Res);
        }


    }


}