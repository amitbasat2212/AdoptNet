using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdoptNet.Data;
using anypet.Models;

namespace AdoptNet.Controllers
{
    public class UserRegsController : Controller
    {
        private readonly AdoptNetContext _context;

        public UserRegsController(AdoptNetContext context)
        {
            _context = context;
        }

        // GET: UserRegs
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserReg.ToListAsync());
        }

        // GET: UserRegs/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userReg = await _context.UserReg
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userReg == null)
            {
                return NotFound();
            }

            return View(userReg);
        }

        // GET: UserRegs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserRegs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmailOfUser,Password,PrivateName,LastName,Age,City,ThereIsAnimal,Id,DateOfCreate")] UserReg userReg)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userReg);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userReg);
        }

        // GET: UserRegs/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userReg = await _context.UserReg.FindAsync(id);
            if (userReg == null)
            {
                return NotFound();
            }
            return View(userReg);
        }

        // POST: UserRegs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("EmailOfUser,Password,PrivateName,LastName,Age,City,ThereIsAnimal,Id,DateOfCreate")] UserReg userReg)
        {
            if (id != userReg.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userReg);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserRegExists(userReg.Id))
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
            return View(userReg);
        }

        // GET: UserRegs/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userReg = await _context.UserReg
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userReg == null)
            {
                return NotFound();
            }

            return View(userReg);
        }

        // POST: UserRegs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var userReg = await _context.UserReg.FindAsync(id);
            _context.UserReg.Remove(userReg);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserRegExists(long id)
        {
            return _context.UserReg.Any(e => e.Id == id);
        }
    }
}
