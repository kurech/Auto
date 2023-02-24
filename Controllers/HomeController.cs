using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using test.Models;

namespace test.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Brands.ToListAsync());
        }
        public IActionResult CreateBrand()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand(Brands brand)
        {
            if (brand.Name == null)
            {
                return Content("Введены не все данные");
            }
            else
            {
                db.Brands.Add(brand);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> EditBrand(int? id)
        {
            if (id != null)
            {
                Brands brand = await db.Brands.FirstOrDefaultAsync(p => p.Id == id);
                if (brand != null)
                    return View(brand);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditBrand(Brands brand)
        {
            if (brand.Name == null)
            {
                return Content("Введены не все данные");
            }
            else
            {
                db.Brands.Update(brand);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Models()
        {
            return View(await db.Models.ToListAsync());
        }

        public IActionResult CreateModel()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateModel(Models.Models models)
        {
            if (models.Name == null)
            {
                return Content("Введены не все данные");
            }
            else
            {
                db.Models.Add(models);
                await db.SaveChangesAsync();
                return RedirectToAction("Models");
            }
        }

        public async Task<IActionResult> EditModel(int? id)
        {
            if (id != null)
            {
                Models.Models models = await db.Models.FirstOrDefaultAsync(p => p.Id == id);
                if (models != null)
                    return View(models);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditModel(Models.Models models)
        {
            if (models.Name == null)
            {
                return Content("Введены не все данные");
            }
            else
            {
                db.Models.Update(models);
                await db.SaveChangesAsync();
                return RedirectToAction("Models");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ModelsByBrand(int? id)
        {
            List<Models.Models> models = new List<Models.Models>();
            await foreach (var a in db.Models)
            {
                if (a.BrandId == id)
                {
                    models.Add(a);
                }
            }
            return View(models);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
