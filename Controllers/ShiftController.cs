using companyProject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class ShiftController : Controller
    {
        private readonly ShiftDAL sd = new ShiftDAL();
        private readonly CompanyDAL _companyDAL = new CompanyDAL();

        public ActionResult Index()
        {

            var s = sd.GetShift();
            return View(s);
        }

        public ActionResult Create()
        {
            
            var companies = _companyDAL.getAllCompany();
            ViewBag.Companies = new SelectList(companies, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShiftModel shift, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        int shiftId = sd.AddShifts(shift);

                        string directoryPath = Server.MapPath("~/Images/SubImages");


                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }

                        string fileName = $"{shiftId}_{Path.GetFileName(file.FileName)}";
                        string filePath = Path.Combine(directoryPath, fileName);


                        file.SaveAs(filePath);

                        shift.imagepath = $"~/Images/SubImages/{fileName}";
                        shift.Id = shiftId;
                        sd.UpdateShift(shift);


                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.ErrorMessage("Please upload an image.");
                    }
                }
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage($"An error occurred: {ex.Message}");
            }
            var companies = _companyDAL.getAllCompany();
            ViewBag.Companies = new SelectList(companies, "Id", "Name");

            return View(shift);
        }




        public ActionResult Edit(int id)
        {
            ShiftModel a = sd.getShiftById(id);
            var companies = _companyDAL.getAllCompany();
            ViewBag.companies = new SelectList(companies, "Id", "Name", a.Company);

            return View(a);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ShiftModel a, HttpPostedFileBase file)
        {
            try
            {
                string directoryPath = Server.MapPath($"~/Images/SubImages/");

                string ExistingFileName = $"{a.Id}_*";

                if (file != null && file.ContentLength > 0)
                {
                    var AlreadyExist = Directory.GetFiles(directoryPath, ExistingFileName);
                    if (AlreadyExist.Length > 0)
                    {
                        System.IO.File.Delete(AlreadyExist[0]);
                    }

                    string fileName = $"{a.Id}_{Path.GetFileName(file.FileName)}";
                    string filePath = Path.Combine(directoryPath, fileName);
                    //string existingFile = Directory.GetFiles(directoryPath, $"{a.Id}_*").FirstOrDefault();
                    //if (existingFile != null)
                    //{
                    //    System.IO.File.Delete(existingFile); // Delete the old file
                    //}

                    file.SaveAs(filePath);
                    a.imagepath = $"~/Images/SubImages/{fileName}";//database me bheja
                }
                sd.UpdateShift(a);
                return RedirectToAction("Index");
            }
            catch 
            {
                return View(a);
            }
        }





        public ActionResult Delete(int id)
        {
            ShiftModel a = sd.getShiftById(id);

            return View(a);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Delete(ShiftModel m)
        {
            try
            {
                sd.DeleteShift(m.Id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


    }
}