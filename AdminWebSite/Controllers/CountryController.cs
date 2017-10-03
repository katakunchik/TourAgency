using AdminWebSite.DAL;
using AdminWebSite.DAL.Entities;
using AdminWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminWebSite.Controllers
{
    public class CountryController : Controller
    {
        EFContext _context;
        public CountryController()
        {
            _context = new EFContext();
        }
        // GET: Country
        public ActionResult Index()
        {
            List<CountryViewModel> model;
            model = _context
                .Countries
                .Select(c=> new CountryViewModel
                {
                    Id=c.Id,
                    Name=c.Name,
                    Priority=c.Priority,
                    DateCreate= c.DateCreate
                })
                .OrderBy(c=>c.Priority)
                .ToList();
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CountryCreateViewModel model)
        {
            Country country = new Country
            {
                DateCreate=DateTime.Now,
                Name=model.Name,
                Priority=model.Priority
            };
            _context.Countries.Add(country);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id = 0)
        {
            Country country = _context.Countries.FirstOrDefault(c => c.Id == id);
            CountryEditViewModel model = new CountryEditViewModel()
            {
                Id = country.Id,
                Name = country.Name,
                Priority = country.Priority,
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(CountryEditViewModel model)
        {
            Country country = _context.Countries.FirstOrDefault(c => c.Id == model.Id);
            country.Name = model.Name;
            country.Priority = model.Priority;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Country country = _context.Countries.FirstOrDefault(c => c.Id == id);
            CountryViewModel model = new CountryViewModel()
            {
                Id = country.Id,
                Name = country.Name,
                Priority = country.Priority,
                DateCreate = country.DateCreate
            };
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Country country = _context.Countries.FirstOrDefault(c => c.Id == id);
            _context.Countries.Remove(country);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id = 0)
        {
            Country country = _context.Countries.FirstOrDefault(c => c.Id == id);
            CountryViewModel model = new CountryViewModel()
            {
                Id = country.Id,
                Name = country.Name,
                Priority = country.Priority,
                DateCreate = country.DateCreate
            };
            return View(model);
        }


    }
}