using companyProject;
using OfficeOpenXml;
using PagedList;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using WebApplication4.Models;
using PagedList.Mvc;
using System.Globalization;
using System;
using System.Linq; 
using Microsoft.Office.Interop.Excel;
using System.Web;
using Newtonsoft.Json;



namespace WebApplication4.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDAL ed = new EmployeeDAL();
        private readonly ShiftDAL shiftDAL = new ShiftDAL();
        private readonly CompanyDAL companyDAL = new CompanyDAL();


        public ActionResult Index(int page = 1)
        {
            List<EmployeeModel> e = ed.GetEmployees();
            if (!e.Any())
            {
                Console.WriteLine("No employees found");
            }

            int employeesPerPage = 3;

            var paginatedEmployees = e.ToPagedList(page, employeesPerPage);

            return View(paginatedEmployees);

        }

        //public ActionResult Index()
        //{

        //    List<EmployeeModel> e = ed.GetEmployees();
        //    return View(e);
        //}

        [HttpPost]
        public FileResult ExportToExcel(List<int> selectedRecords)
        {
           
            Console.WriteLine("Selected Records: " + string.Join(", ", selectedRecords));
            List<EmployeeModel> e = ed.GetEmployees();

            var newList = new List<EmployeeModel>();

            foreach(var emp in e)
            {
                if (selectedRecords.Contains(emp.Id))
                {
                    newList.Add(emp);
                }

            }


            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");
                worksheet.Cells[1, 1].Value = "Id";
                worksheet.Cells[1, 2].Value = "Name";
                worksheet.Cells[1, 3].Value = "MobileNumber";
                worksheet.Cells[1, 4].Value = "email";
                worksheet.Cells[1, 5].Value = "Address";
                worksheet.Cells[1, 6].Value = "ShiftName";
                worksheet.Cells[1, 7].Value = "CompanyName";
                worksheet.Cells[1, 8].Value = "Country";
                worksheet.Cells[1, 9].Value = "State";
                worksheet.Cells[1, 10].Value = "City";
                //worksheet.Cells.LoadFromCollection(e,true);

                int row = 2;

                foreach(var emp in newList)
                {
                    worksheet.Cells[row, 1].Value = emp.Id;
                    worksheet.Cells[row, 2].Value = emp.Name;
                    worksheet.Cells[row, 3].Value = emp.MobileNumber;
                    worksheet.Cells[row, 4].Value = emp.email;
                    worksheet.Cells[row, 5].Value = emp.Address;
                    worksheet.Cells[row, 6].Value = emp.ShiftName;
                    worksheet.Cells[row, 7].Value = emp.CompanyName;
                    worksheet.Cells[row, 8].Value = emp.Country;
                    worksheet.Cells[row, 9].Value = emp.State;
                    worksheet.Cells[row, 10].Value = emp.City;
                    

                    row++;

                }
                package.Save();
            }
            stream.Position = 0;
            string excelName = $"EmployeeList.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);

        }


        public JsonResult GetEmployeeDataForExport()
        {
            List<EmployeeModel> employees = ed.GetEmployees();

            return Json(employees, JsonRequestBehavior.AllowGet);
        }

     
    













        public ActionResult Create()
        {
            var companies = companyDAL.getAllCompany();
            ViewBag.fk_CompanyID = new SelectList(companies, "Id", "Name");


            var shifts = shiftDAL.GetShift();
            ViewBag.fk_ShiftId = new SelectList(shifts, "Id", "Name");

            var countries = ed.GetCountries();
            ViewBag.fk_CountryId = new SelectList(countries, "CountryId", "CountryName");


            ViewBag.fk_StateId = new SelectList(new List<StateModel>(), "StateId", "StateName");
            ViewBag.fk_CityId = new SelectList(new List<CityModel>(), "Id", "Name");


            return View();
        }

        public JsonResult GetShiftEndReasons()
        {
            List<EmployeeModel> e = ed.GetShiftEndReasons();
            return Json(e, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStates(int countryId)
        {
            List<StateModel> states = ed.GetStateByCountry(countryId);
            return Json(states, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCities(int stateId)
        {
            List<CityModel> cities = ed.GetCityByState(stateId);
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public ActionResult Create(EmployeeModel model, string ShiftEndReasonIds, string ShiftEndTime)
        //{
        //    try
        //    {
        //        // Step 1: Save the Employee first (without ShiftEndReasonId and ShiftEndTime)
        //        var employee = new EmployeeModel
        //        {
        //            Name = model.Name,
        //            MobileNumber = model.MobileNumber,
        //            email = model.email,
        //            Address = model.Address,
        //            fk_ShiftId = model.fk_ShiftId,
        //            fk_CompanyID = model.fk_CompanyID,
        //            FK_Country_Id = model.FK_Country_Id,
        //            FK_State_Id = model.FK_State_Id,
        //            FK_City_Id = model.FK_City_Id

        //        };


        //        ed.AddEmployee(employee); 

        //        int reasonId = int.Parse(ShiftEndReasonIds);


        //        DateTime shiftEndTime = DateTime.ParseExact(ShiftEndTime, "HH:mm", CultureInfo.InvariantCulture);


        //        employee.FK_ShiftEndReasonId = reasonId;
        //        employee.ShiftEndTime = shiftEndTime;

        //        ed.UpdateEmployee(employee);  

        //        return RedirectToAction("Index"); 
        //    }
        //    catch 
        //    {
        //        return Json(false);
        //    }
        //}








        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeModel c)
        {

          
            try
            {
                System.Diagnostics.Debug.WriteLine("Received Time: " + c.ShiftEndTime);

                Console.WriteLine("Received Time: " + c.ShiftEndTime);
                ed.AddEmployee(c);
                return RedirectToAction("Index");

            }
            catch
            {
                return View();


            }
        }



public ActionResult DownloadTemplate()
    {
        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("EmployeeTemplate");

        
            worksheet.Cells[1, 1].Value = "Name";
            worksheet.Cells[1, 2].Value = "MobileNumber";
            worksheet.Cells[1, 3].Value = "Address";

            worksheet.Column(1).Width = 20;
            worksheet.Column(2).Width = 15;
            worksheet.Column(3).Width = 30;

            
            var stream = new MemoryStream();
            package.SaveAs(stream);
            stream.Position = 0;

            string fileName = "EmployeeFormat.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }

       
        [HttpPost]
        public ActionResult UploadExcel(HttpPostedFileBase fileUpload)
        {
            try
            {
                if (fileUpload != null && fileUpload.ContentLength > 0)
                {
                    string fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();

                    if (fileExtension == ".xls" || fileExtension == ".xlsx")
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            fileUpload.InputStream.CopyTo(memoryStream);
                            memoryStream.Position = 0;

                            using (var package = new ExcelPackage(memoryStream))
                            {
                                var worksheet = package.Workbook.Worksheets[0];
                                int rowCount = worksheet.Dimension.Rows;

                                List<EmployeeModel> allRecords = new List<EmployeeModel>();
                                List<EmployeeModel> duplicateRecords = new List<EmployeeModel>();
                                Dictionary<string, int> mobileCount = new Dictionary<string, int>();




                                for (int row = 2; row <= rowCount; row++)
                                {
                                    string Name = worksheet.Cells[row, 1].Text;
                                    string mobileNo = worksheet.Cells[row, 2].Text;
                                    string Address = worksheet.Cells[row, 3].Text;

                                    var employee = new EmployeeModel
                                    {
                                        Name = Name,
                                        MobileNumber = mobileNo,
                                        Address = Address
                                    };


                                    allRecords.Add(employee);


                                    if (mobileCount.ContainsKey(mobileNo))
                                    {
                                        mobileCount[mobileNo]++;     // same number appeared more than once to fir increase its count
                                    }
                                    else
                                    {
                                        mobileCount[mobileNo] = 1;
                                    }
                                }


                                foreach (var employee in allRecords)
                                {

                                    if (mobileCount[employee.MobileNumber] > 1)
                                        duplicateRecords.Add(employee);

                                }

                                var nonDuplicateRecords = allRecords.Except(duplicateRecords).ToList();
                                string jsonData = JsonConvert.SerializeObject(nonDuplicateRecords);
                                ed.AddEmployee2(jsonData);


                                if (duplicateRecords.Any())
                                {
                                    using (var duplicatePackage = new ExcelPackage())
                                    {
                                        var duplicateWorksheet = duplicatePackage.Workbook.Worksheets.Add("Duplicates");
                                        duplicateWorksheet.Cells[1, 1].Value = "Name";
                                        duplicateWorksheet.Cells[1, 2].Value = "Mobile Number";
                                        duplicateWorksheet.Cells[1, 3].Value = "Address";

                                        int rowIndex = 2;
                                        foreach (var duplicate in duplicateRecords)
                                        {
                                            duplicateWorksheet.Cells[rowIndex, 1].Value = duplicate.Name;
                                            duplicateWorksheet.Cells[rowIndex, 2].Value = duplicate.MobileNumber;
                                            duplicateWorksheet.Cells[rowIndex, 3].Value = duplicate.Address;
                                            rowIndex++;
                                        }

                                        byte[] fileContent = duplicatePackage.GetAsByteArray();
                                        return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DuplicateRecords.xlsx");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Please upload an Excel file.";
                    }
                }
                else
                {
                    ViewBag.Message = "Please choose a file to upload.";
                }

                return RedirectToAction("Create");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: " + ex.Message;
                return View();
            }
        }
       



        //[HttpPost]
        //public ActionResult UploadExcel(HttpPostedFileBase fileUpload)
        //{
        //    try
        //    {
        //        if (fileUpload != null && fileUpload.ContentLength > 0)
        //        {
        //            string fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();

        //            if (fileExtension == ".xls" || fileExtension == ".xlsx")
        //            {
        //                using (var memoryStream = new MemoryStream())
        //                {
        //                    fileUpload.InputStream.CopyTo(memoryStream);
        //                    memoryStream.Position = 0;

        //                    using (var package = new ExcelPackage(memoryStream))
        //                    {
        //                        var worksheet = package.Workbook.Worksheets[0];
        //                        int rowCount = worksheet.Dimension.Rows;

        //                        List<EmployeeModel> duplicateRecords = new List<EmployeeModel>();
        //                        HashSet<string> existingMobiles = new HashSet<string>();

        //                        for (int row = 2; row <= rowCount; row++)
        //                        {
        //                            string Name = worksheet.Cells[row, 1].Text;
        //                            string mobileNo = worksheet.Cells[row, 2].Text;
        //                            string Address = worksheet.Cells[row, 3].Text;

        //                            var employee = new EmployeeModel
        //                            {
        //                                Name = Name,
        //                                MobileNumber = mobileNo,
        //                                Address = Address
        //                            };

        //                            if (existingMobiles.Contains(mobileNo))
        //                            {
        //                               // duplicateRecords.Add(new EmployeeModel { Name = Name, MobileNumber = mobileNo, Address = Address });
        //                                duplicateRecords.Add(employee);

        //                            }
        //                            else
        //                            {
        //                                existingMobiles.Add(mobileNo);
        //                                // ed.AddEmployee2(new EmployeeModel { Name = Name, MobileNumber = mobileNo, Address = Address });
        //                                ed.AddEmployee2(employee);
        //                            }
        //                        }


        //                        if (duplicateRecords.Any())
        //                        {
        //                            using (var duplicatePackage = new ExcelPackage())
        //                            {
        //                                var duplicateWorksheet = duplicatePackage.Workbook.Worksheets.Add("Duplicates");
        //                                duplicateWorksheet.Cells[1, 1].Value = "Name";
        //                                duplicateWorksheet.Cells[1, 2].Value = "Mobile Number";
        //                                duplicateWorksheet.Cells[1, 3].Value = "Address";

        //                                int rowIndex = 2;
        //                                foreach (var duplicate in duplicateRecords)
        //                                {
        //                                    duplicateWorksheet.Cells[rowIndex, 1].Value = duplicate.Name;
        //                                    duplicateWorksheet.Cells[rowIndex, 2].Value = duplicate.MobileNumber;
        //                                    duplicateWorksheet.Cells[rowIndex, 3].Value = duplicate.Address;
        //                                    rowIndex++;
        //                                }
        //                                byte[] fileContent = duplicatePackage.GetAsByteArray();               //saving in memory only epplus not that seperate method  to getduplicates by using this helps store byte array allows to keep everything in memory only thats why prefered 
        //                                return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DuplicateRecords.xlsx");
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                ViewBag.Message = "Please upload an Excel file.";
        //            }
        //        }
        //        else
        //        {
        //            ViewBag.Message = "No file selected. Please choose a file to upload.";
        //        }

        //        return RedirectToAction("Create");
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Message = "Error: " + ex.Message;
        //        return View();
        //    }
        //}




        //[HttpPost]
        //public ActionResult UploadExcel(HttpPostedFileBase fileUpload)
        //{
        //    try
        //    {
        //        if (fileUpload != null && fileUpload.ContentLength > 0)
        //        {
        //            string fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();

        //            if (fileExtension == ".xls" || fileExtension == ".xlsx")
        //            {
        //                using (var memoryStream = new MemoryStream())
        //                {
        //                    fileUpload.InputStream.CopyTo(memoryStream);
        //                    memoryStream.Position = 0;


        //                    using (var package = new ExcelPackage(memoryStream))
        //                    {
        //                        var worksheet = package.Workbook.Worksheets[0];
        //                        int rowCount = worksheet.Dimension.Rows;


        //                        for (int row = 2; row <= rowCount; row++)
        //                        {

        //                            string Name = worksheet.Cells[row, 1].Text;
        //                            string mobileNo = worksheet.Cells[row, 2].Text;
        //                            string Address = worksheet.Cells[row, 3].Text;


        //                            EmployeeModel employee = new EmployeeModel
        //                            {
        //                                Name = Name,
        //                                MobileNumber = mobileNo,
        //                                Address = Address
        //                            };




        //                            ed.AddEmployee2(employee);
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                ViewBag.Message = "not present.";
        //            }
        //        }

        //            return RedirectToAction("Create"); 

        //    }
        //    catch(Exception ex)
        //    {

        //        ViewBag.Message = "Error iss " + ex.Message;
        //        Console.WriteLine(ex.ToString());
        //        return View();
        //    }
        //}














        //public ActionResult Create(EmployeeModel employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ed.AddEmployee(employee);
        //        return RedirectToAction("Index");
        //    }
        //    var companies = companyDAL.getAllCompany();
        //    ViewBag.fk_CompanyID = new SelectList(companies, "Id", "Name", employee.fk_CompanyID);

        //    var shifts = shiftDAL.GetShift();
        //    ViewBag.fk_ShiftId = new SelectList(shifts, "Id", "Name", employee.fk_ShiftId);

        //    return View(employee);
        //}
        public ActionResult Edit(int id)
        {
           
            var employee = ed.getEmployeeById(id);
            //var company = companyDAL.getCompanyById(id);
            //var shift = shiftDAL.getShiftById(id);

            var companies = companyDAL.getAllCompany();
            var shifts = shiftDAL.GetShift();


            var countries = ed.GetCountries();
            var states = ed.GetStateByCountry(employee.FK_Country_Id);
            var city = ed.GetCityByState(employee.FK_State_Id);
           
            ViewBag.fk_CountryId = new SelectList(countries, "CountryId", "CountryName",employee.FK_Country_Id);
            ViewBag.fk_StateId = new SelectList(states, "StateId", "StateName",employee.FK_State_Id);
            ViewBag.fk_CityId = new SelectList(city, "Id", "Name", employee.FK_City_Id);


            ViewBag.fk_CompanyID = new SelectList(companies, "Id", "Name",employee.fk_CompanyID);
            ViewBag.fk_ShiftID = new SelectList(shifts, "Id", "Name",employee.fk_ShiftId);

           


            return View(employee); 
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(EmployeeModel a)
        {
            try
            {
                ed.UpdateEmployee(a);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(a);
            }


            //var companies = companyDAL.getAllCompany();
            //var shifts = shiftDAL.GetShift();
            //var countries = ed.GetCountries();





            //ViewBag.fk_CompanyID = new SelectList(companies, "Id", "Name", a.fk_CompanyID);
            //ViewBag.fk_ShiftID = new SelectList(shifts, "Id", "Name", a.fk_ShiftId);
            //ViewBag.fk_CountryId = new SelectList(countries, "CountryId", "CountryName", a.FK_Country_Id);
            //ViewBag.fk_StateId = new SelectList(new List<StateModel>(), "StateId", "StateName", a.FK_State_Id);
            //ViewBag.fk_CityId = new SelectList(new List<CityModel>(), "Id", "Name", a.FK_City_Id);

            //return View(a);
        }

        public ActionResult Delete(int id)
        {
            EmployeeModel a = ed.getEmployeeById(id);

            return View(a);


        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Delete(EmployeeModel e)
        {
            try
            {
                ed.DeleteEmployee(e.Id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}