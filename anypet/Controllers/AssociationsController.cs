﻿using System;
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
            return View(await _context.Association.ToListAsync());
        }

        // GET: Associations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var association = await _context.Association
                .FirstOrDefaultAsync(m => m.ID == id);
            if (association == null)
            {
                return NotFound();
            }

            return View(association);
        }

        // GET: Associations/Create
        public IActionResult Create()
        {
            ViewData["Animals"] = new SelectList(_context.Animal.Where(x => x.IdAssociation==null), nameof(Animal.ID), nameof(Animal.Name));
            return View();
        }

        // POST: Associations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,PhoneNumber,Location,EmailOfUser")] Association association)
        {
            if (ModelState.IsValid)
            {
               
                _context.Add(association);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(association);
        }

        // GET: Associations/Edit/5
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
            return View(association);
        }

        // POST: Associations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,PhoneNumber,Location,EmailOfUser")] Association association)
        {
            if (id != association.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(association);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssociationExists(association.ID))
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
            return View(association);
        }

        // GET: Associations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var association = await _context.Association
                .FirstOrDefaultAsync(m => m.ID == id);
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
            return _context.Association.Any(e => e.ID == id);
        }
    }
}
