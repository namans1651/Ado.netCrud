using companyProject;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        private readonly CompanyDAL cd = new CompanyDAL();

        public ActionResult Index()
        {
            List<CompanyModel> c = cd.getAllCompany();
            return View(c);
        }
        public ActionResult Create()
        {
                var list = new List<string>
            {
                "Sikkim", "Tamil Nadu", "Telangana", "Tripura", "Uttar Pradesh", "Uttarakhand"
            };
               ViewBag.list = list;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CompanyModel company)
        {
            try
            {
                

                var companyJson = JsonConvert.SerializeObject(company);

                ViewBag.CompanyData = JsonConvert.SerializeObject(company);

                cd.AddCompany(companyJson);
                
                return RedirectToAction("Index");
            }

            catch (Exception ex)
            {
               
                ViewBag.ErrorMessage = "Exception: " + ex.Message;
                return View(); 
            }
            }
     
            
            




















    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public JsonResult Create(CompanyModel company)
    //{
    //    try
    //    {

    //        cd.AddCompany(company); 

    //        return Json(true);
    //    }
    //    catch
    //    {

    //        return Json(false);
    //    }
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

    public ActionResult Edit(int id)
        {
            CompanyModel a = cd.getCompanyById(id);

            return View(a);
            //  return RedirectToAction("Index");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(CompanyModel a)
        {
            try
            {
                cd.UpdateCompany(a);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            CompanyModel a = cd.getCompanyById(id);

            return View(a);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Delete(CompanyModel m)
        {
            try
            {
                cd.DeleteCompany(m.Id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return RedirectToAction("Index", "Employee");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}