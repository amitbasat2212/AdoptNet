using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdoptNet.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using anypet.Models;

namespace Ad.Controllers
{
    public class UsersController : Controller
    {
        private readonly AdoptNetContext _context;

        public UsersController(AdoptNetContext context)
        {
            _context = context;
        }

        //this isthe get of login & access denied
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        // POST: Users/Login
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Id,Username,Password")] User user) //login func
        {
            if (ModelState.IsValid)
            {
                var q = from u in _context.User
                        where u.Username == user.Username && u.Password == user.Password
                        select u;

                if (q.Count() > 0)
                {

                    Signin(q.First()); //sendind the first user to signin

                    return RedirectToAction(nameof(Index), "Home");
                }
                else
                {
                    ViewData["Error"] = "Username and/or password are incorrect.";
                }
            }
            return View(user);
        }

        private async void Signin(User account)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, account.Username),
                    new Claim(ClaimTypes.Role, account.Type.ToString()),
                };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
        // GET: Users/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Users/Register
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,Username,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                var q = _context.User.FirstOrDefault(u => u.Username == user.Username); // if return null- there is no user like this ans we can do register
                              
                   
                if (q == null)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();

                    var u = _context.User.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);

                    Signin(u);

                    return RedirectToAction(nameof(Index), "Home");// regsiter succeed- go to home
                }
                else // user already exists
                {
                    ViewData["Error"] = "Unable to comply; cannot register this user.";
                }
            }
            return View(user);
        }


        public async Task<IActionResult> Logout()
        {
            //HttpContext.Session.Clear();

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(Index), "Home");
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Password,Type")] User user)
        {
            if (ModelState.IsValid)
            {
                var q = _context.User.FirstOrDefault(u => u.Username == user.Username); // if return null- there is no user like this ans we can do register

                if (q == null)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                else // user already exists
                {
                    ViewData["Error"] = "Unable to comply; this user exist.";
                }
            }
            return View(user);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //// GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }


        //        // POST: Users/Edit/5
        //        // To protect from overposting attacks, enable the specific properties you want to bind to.
        //        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Password,Type")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        public async Task<IActionResult> Search(String Searching)
        {  // Use LINQ to get list of genres.
            Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable<anypet.Models.User> SearchContent = null;
            var user = _context.User.Where(b => (b.Username.Equals(Searching) || Searching == null));
            if (Searching == null)
            {
                
                return View("Index", await user.ToListAsync());
            }

            if (Searching.Equals("Client") || Searching.Equals("Admin") || Searching.Equals("Association"))
            {
                UserType k = (UserType)Enum.Parse(typeof(UserType), Searching);
                SearchContent = (Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable<User>)_context.User.Where(a => a.Type.Equals(k));
                return View("Index", await SearchContent.ToListAsync());

            }           


            return View("Index", await user.ToListAsync());
        }

    }

}