using AdminWebSite.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using AdminWebSite.Models;
using AdminWebSite.DAL.Entities;

namespace AdminWebSite.Controllers
{
    public class CityController : Controller
    {
        EFContext _context;
        public CityController()
        {
            _context = new EFContext();
        }
        // GET: City
        public ActionResult Index()
        {
            var model = _context
                .Cities.Include(c => c.Country)
                .Select(c => new CityViewModel
                {
                    Id=c.Id,
                    Name=c.Name,
                    Priority=c.Priority,
                    DateCreate=c.DateCreate,
                    Country=c.Country.Name
                });
            return View(model);
        }
        public ActionResult Create()
        {
            CityCreateViewModel model = new CityCreateViewModel();
            model.Countries = _context.Countries
                .Select(c=>new SelectItemViewModel
                {
                    Id=c.Id,
                    Name=c.Name
                }).ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(CityCreateViewModel model)
        {
            City city = new City
            {
                Name=model.Name,
                DateCreate=DateTime.Now,
                Priority=model.Priority,
                CountryId=model.CountryId
            };
            _context.Cities.Add(city);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            City city = _context.Cities.FirstOrDefault(c => c.Id == id);
            CityEditViewModel model = new CityEditViewModel()
            {
                Id = city.Id,
                Name = city.Name,
                Priority = city.Priority,
                CountryId = city.CountryId,
                Countries = _context.Countries
                .Select(c => new SelectItemViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(CityEditViewModel model)
        {
            City city = _context.Cities.FirstOrDefault(c => c.Id == model.Id);
            city.Name = model.Name;
            city.Priority = model.Priority;
            city.CountryId = model.CountryId;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}