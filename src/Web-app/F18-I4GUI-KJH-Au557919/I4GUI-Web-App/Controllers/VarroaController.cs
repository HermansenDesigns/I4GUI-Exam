using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using I4GUIWebApp.Models;
using I4GUIWebApp.Models.VarroaViewModels;
using I4GUI_Web_App.Data;
using I4GUI_Web_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace I4GUI_Web_App.Controllers
{
    [Authorize]
    public class VarroaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        public VarroaController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        // GET: Varroa
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            return View(_context.VarroaCount.Where(i => i.User.Email == user.Email));
        }

        // GET: Varroa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var varroaCount = await _context.VarroaCount.SingleOrDefaultAsync(m => m.Id == id);

            if (varroaCount == null)
            {
                return NotFound();
            }
            varroaCount.User = await _context.User.SingleOrDefaultAsync(m => m.Id == varroaCount.UserId);

            return View(varroaCount);
        }

        // GET: Varroa/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Varroa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Beehive,DOC,Varroa,ObservationLength,Comments")] CreationViewModel varroaCount)
        {
            if (!ModelState.IsValid)
                return View(varroaCount);

            var user = await _userManager.GetUserAsync(User);

            var shortUser = new User
            {
                Email = user.Email,
                ZipCode = user.ZipCode
            };
            varroaCount.User = shortUser;

            var result = new VarroaCount()
            {
                User = varroaCount.User,
                Id = varroaCount.Id,
                Varroa = varroaCount.Varroa,
                UserId = varroaCount.UserId,
                Beehive = varroaCount.Beehive,
                Comments = varroaCount.Comments,
                DOC = varroaCount.DOC,
                ObservationLength = varroaCount.ObservationLength
            };


            _context.Add(result);
            _context.Add(shortUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Varroa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var varroaCount = await _context.VarroaCount.SingleOrDefaultAsync(m => m.Id == id);
            if (varroaCount == null)
            {
                return NotFound();
            }

            var model = new CreationViewModel()
            {
                User = varroaCount.User,
                Id = varroaCount.Id,
                Varroa = varroaCount.Varroa,
                UserId = varroaCount.UserId,
                ObservationLength = varroaCount.ObservationLength,
                Beehive = varroaCount.Beehive,
                DOC = varroaCount.DOC,
                Comments = varroaCount.Comments
            };

            return View(model);
        }

        // POST: Varroa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Beehive,DOC,Varroa,ObservationLength,Comments")] CreationViewModel varroaCount)
        {
            if (id != varroaCount.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(varroaCount);

            var model = new CreationViewModel()
            {
                User = varroaCount.User,
                Id = varroaCount.Id,
                Varroa = varroaCount.Varroa,
                UserId = varroaCount.UserId,
                ObservationLength = varroaCount.ObservationLength,
                Beehive = varroaCount.Beehive,
                DOC = varroaCount.DOC,
                Comments = varroaCount.Comments
            };

            try
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VarroaCountExists(model.Id))
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

        // GET: Varroa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var varroaCount = await _context.VarroaCount
                .SingleOrDefaultAsync(m => m.Id == id);
            if (varroaCount == null)
            {
                return NotFound();
            }

            return View(varroaCount);
        }

        // POST: Varroa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var varroaCount = await _context.VarroaCount.SingleOrDefaultAsync(m => m.Id == id);
            _context.VarroaCount.Remove(varroaCount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VarroaCountExists(int id)
        {
            return _context.VarroaCount.Any(e => e.Id == id);
        }
    }
}
