using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using I4GUIWebApp.Models;
using I4GUIWebApp.Models.HomeViewModels;
using I4GUI_Web_App.Data;
using Microsoft.AspNetCore.Mvc;
using I4GUI_Web_App.Models;
using Microsoft.EntityFrameworkCore;

namespace I4GUI_Web_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.VarroaCount.ToListAsync();
            foreach (var varroaCount in list)
            {
                varroaCount.User = _context.User.SingleOrDefault(m => m.Id == varroaCount.UserId);
            }

            var zipCodes = new List<string>();
            Parallel.ForEach<VarroaCount>(list, obj =>
            {
                var zip = obj.User.ZipCode;

                if (!zipCodes.Contains(zip))
                    zipCodes.Add(zip);
            });

            var listOfValues = new List<(string, int)>();

            Parallel.ForEach(zipCodes, (zip) =>
            {
                var varroaCounts = _context.VarroaCount.Where(i => i.User.ZipCode == zip);
                var sum = 0;
                foreach (var varroaCount in varroaCounts)
                {
                    sum += varroaCount.Varroa;
                }

                var median = sum / varroaCounts.Count();


                listOfValues.Add((zip, median));
            });

            var varroaList = new VarroaListViewModel()
            {
                Varroa = listOfValues
            };

            return View(varroaList);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
