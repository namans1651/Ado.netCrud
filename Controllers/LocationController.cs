using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class LocationController : Controller
    {
        private readonly CountryDAL _countryDal = new CountryDAL();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddCountry(CountryModel model)
        {
            try
            {
                    _countryDal.AddCountry(model);
                   
                    return Json(true);
            }
            catch
            {
                return Json(false);
            }
        }







        public ActionResult Create()
        {
            var countries = new List<CountryModel>
        {
            new CountryModel { CountryId = 1, CountryName = "India" }
        };

            // States for India
            var states = new List<StateModel>
        {
            new StateModel { StateId = 1, StateName = "Delhi", CountryId = 1 },
            new StateModel { StateId = 2, StateName = "Uttar Pradesh", CountryId = 1 },
            new StateModel { StateId = 3, StateName = "Maharashtra", CountryId = 1 }
        };

            // Pass country and states to ViewBag
            ViewBag.Country = new SelectList(countries, "CountryId", "CountryName");
            ViewBag.State = new SelectList(states, "StateId", "StateName");

            return View();
        }

        [HttpPost]
        public ActionResult Create(EmployeeModel model)
        {
            // Here, model.fk_Country_Id and model.fk_State_Id will have the selected IDs
            // Save the data as per your requirement

            return RedirectToAction("Index");
        }
    }
}