using companyProject;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Web.DynamicData;
using System.Web.Helpers;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class MapController : Controller
    {
        private readonly CountryDAL _countryDal = new CountryDAL();
        private readonly EmployeeDAL _employeeDal = new EmployeeDAL();

        // GET: Map/Create

        public ActionResult Index()
        {
            List<CountryModel> c = _countryDal.GetCountry();
            //   ViewBag.CountryList = new SelectList()
            return View(c);
        }
        public JsonResult Country()
        {
            List<CountryModel> c = _countryDal.GetCountry();
            return Json(c, JsonRequestBehavior.AllowGet);
        }

        public JsonResult State(int id)
        {
            var st = _countryDal.GetStateByCountry(id);
            return Json(st, JsonRequestBehavior.AllowGet);
        }
    }
}




        //public ActionResult Create()
        //{
        //    ViewBag.list = list;
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public ActionResult Create(CompanyModel c)
        //{
        //    try
        //    {
        //        var list = new List<string>
        //    {
        //        "Sikkim", "Tamil Nadu", "Telangana", "Tripura", "Uttar Pradesh", "Uttarakhand"
        //    };
        //        ViewBag.list = list;

        //        cd.AddCompany(c);
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //public ActionResult Create()
        //{

        //    var countries = _countryDal.GetCountry();
        //    ViewBag.Countries = new SelectList(countries, "CountryId", "CountryName");


        //    var states = _stateDal.GetStateByCountry(1); 
        //    ViewBag.States = new SelectList(states, "StateId", "StateName");

        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(EmployeeModel model)
        //{
        //    try
        //    {

        //        _employeeDal.AddEmployee(model);

        //        return RedirectToAction("Index"); 
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //public ActionResult GetStatesByCountry(int countryId)
        //{

        //    var states = _stateDal.GetStateByCountry(countryId);


        //    ViewBag.States = new SelectList(states, "StateId", "StateName");

        //    // Re-populate country dropdown in case the selected country is lost
        //    var countries = _countryDal.GetCountry();
        //    ViewBag.Countries = new SelectList(countries, "CountryId", "CountryName");

        //    return View("Create");
        //}
 
