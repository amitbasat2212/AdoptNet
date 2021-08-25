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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using anypet.Controllers;

namespace AdoptNet.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly AdoptNetContext _context;
        public AnimalsController(AdoptNetContext context)
        {
            _context = context;
        }


        //function for graph 




        public async Task<IActionResult> Search(String Searching)
        {

            Kind k;
            Location l;
            Gender g;
            Size s;


            Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable<anypet.Models.Animal> SearchContent = null;


            if (Searching.Equals("Cat") || Searching.Equals("Dog"))
            {
                k = (Kind)Enum.Parse(typeof(Kind), Searching);
                SearchContent = (Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable<Animal>)_context.Animal.Include(x => x.AnimalImage).Include(a => a.Association).Where(a => a.Kind.Equals(k));

            }
            else if (Searching.Equals("Center") || Searching.Equals("North") || Searching.Equals("South"))
            {
                l = (Location)Enum.Parse(typeof(Location), Searching);
                SearchContent = (Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable<Animal>)_context.Animal.Include(x => x.AnimalImage).Include(a => a.Association).Where(a => a.Location.Equals(l));
            }
            else if (Searching.Equals("Male") || Searching.Equals("Female"))
            {
                g = (Gender)Enum.Parse(typeof(Gender), Searching);
                SearchContent = (Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable<Animal>)_context.Animal.Include(x => x.AnimalImage).Include(a => a.Association).Where(a => a.Gender.Equals(g));
            }
            else if (Searching.Equals("Small") || Searching.Equals("Medium") || Searching.Equals("Big"))
            {
                s = (Size)Enum.Parse(typeof(Size), Searching);
                SearchContent = (Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable<Animal>)_context.Animal.Include(x => x.AnimalImage).Include(a => a.Association).Where(a => a.Size.Equals(s));
            }


            if (SearchContent == null)
            {
                return View("Index", new List<Animal>());
            }

            //var SearchContent = _context.Animal.Where(a => 1==1);

            //var q = "SELECT * FROM dbo.Animal WHERE [Name] LIKE '%" + Searching + "%' OR [Description] LIKE '%" + Searching + "%'";
            //var SearchContent = _context.Animal.FromSqlRaw(q);

            return View("Index", await SearchContent.ToListAsync());


        }


        [Authorize(Roles = "Admin,Association,Client")]
        public async Task<IActionResult> SearchDog()
        {
            Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable<anypet.Models.Animal> SearchContent = null;
            Kind k;

            k = (Kind)Enum.Parse(typeof(Kind), "Dog");
            SearchContent = (Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable<Animal>)_context.Animal.Include(x => x.AnimalImage).Include(a => a.Association).Where(a => a.Kind.Equals(k));

            return View("Index", await SearchContent.ToListAsync());
        }


        [Authorize(Roles = "Admin,Association,Client")]
        public async Task<IActionResult> SearchCat()
        {
            Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable<anypet.Models.Animal> SearchContent = null;
            Kind k;

            k = (Kind)Enum.Parse(typeof(Kind), "Cat");
            SearchContent = (Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable<Animal>)_context.Animal.Include(x => x.AnimalImage).Include(a => a.Association).Where(a => a.Kind.Equals(k));

            return View("Index", await SearchContent.ToListAsync());
        }


        // GET: Animals
        [Authorize(Roles = "Admin,Association,Client")]
        public async Task<IActionResult> Index()
        {
            var adoptNetContext = (from Al in _context.Animal
                                   join As in _context.Association
                                   on Al.AssociationId equals As.Id
                                   select new Animal
                                   {
                                       Id = Al.Id,
                                       AssociationId = As.Id,
                                       Name = Al.Name,
                                       Kind = Al.Kind,
                                       Age = Al.Age,
                                       Gender = Al.Gender,
                                       Description = Al.Description,
                                       Size = Al.Size,
                                       Location = Al.Location,
                                       AnimalImage = Al.AnimalImage,

                                       Association = As

                                   }
                                   );



            //var adoptNetContext = _context.Animal.Include(a => a.Association).Include(a=>a.AnimalImage);
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
        [Authorize(Roles = "Admin,Association")]
        public IActionResult Create()
        {
            ViewData["AssociationId"] = new SelectList(_context.Association, "Id", nameof(Association.Name));
            return View();
        }

        //
        // new part connecting to twitter api
        public static async Task<String> PostMessageToTwitter(string animalName)
        {
            string ConsumerKey = "ArKksYuZtj2RS4MdvZRQtMNBH";
            string ConsumerKeySecret = "XCibkNO5mdsNXNuv114sw9A9HttGUq8wVTy9wOo7l9zfQSL6dn";
            string AccessToken = "1429780751516635138-ANSpo1Rojk9VDOSN4gdzPSeDPPmyAE";
            string AccessTokenSecret = "xsonrP8PHCSE3vy35druX5S62Jx8O4DJKCNfrbKmnqRDO";

            var twitter = new TwitterAPI(ConsumerKey,
                ConsumerKeySecret, AccessToken, AccessTokenSecret);

            string message = "New animal has been added, come in to our website to see " +
                "more about: " + animalName + " and maybe adopt this animal! :-)";

            var response = await twitter.Tweet(message);
            Console.WriteLine(response);

            return response;
        }

        //

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
                PostMessageToTwitter(animal.Name).Wait();
                return RedirectToAction(nameof(Index));
            }

            ViewData["AssociationId"] = new SelectList(_context.Association, "Id", nameof(Association.Name));
            return View(animal);

        }

        // GET: Animals/Edit/5
        [Authorize(Roles = "Admin,Association")]
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
        [Authorize(Roles = "Admin,Association")]
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