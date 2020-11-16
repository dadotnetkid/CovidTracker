using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Repository;
using Microsoft.AspNet.Identity;
using Helpers;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Ajax.Utilities;
using Models;

namespace Web.Controllers
{
    [RoutePrefix("patients")]
    public class PatientsController : Controller
    {
        // GET: Patients
        private UnitOfWork unitOfWork = new UnitOfWork();

        #region batch

        public ActionResult Batch()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult BatchGridViewPartial()
        {
            var model = unitOfWork.BatchNumbersRepo.Get(includeProperties: "CreatedByUser");
            return PartialView("_BatchGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult BatchGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))]
            Models.BatchNumbers item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    item.BatchNumber = new Random().Next(1, 1000000).ToString("D6");
                    item.CreatedAt = DateTime.Now;
                    item.CreatedBy = User.Identity.GetUserId();
                    unitOfWork.BatchNumbersRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";

            var model = unitOfWork.BatchNumbersRepo.Get(includeProperties: "CreatedByUser");
            return PartialView("_BatchGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult BatchGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))]
            Models.BatchNumbers item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";

            var model = unitOfWork.BatchNumbersRepo.Get(includeProperties: "CreatedByUser");
            return PartialView("_BatchGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult BatchGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]
            System.Int32 Id)
        {
            if (Id >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    unitOfWork.BatchNumbersRepo.Delete(x => x.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }

            var model = unitOfWork.BatchNumbersRepo.Get(includeProperties: "CreatedByUser");
            return PartialView("_BatchGridViewPartial", model);
        }

        #endregion

        #region Add Case

        [OnUserAuthorization(ActionName = "Add Case")]
        [Route("batch/{batchnumber}")]
        public ActionResult AddCases(string batchNumber)
        {
            ViewBag.Title = "Batch: " + batchNumber;
            var batch = unitOfWork.BatchNumbersRepo.Find(x => x.BatchNumber == batchNumber);
            return View(batch);
        }

        [ValidateInput(false)]
        public ActionResult PatientsGridViewPartial([ModelBinder(typeof(DevExpressEditorsBinder))]
            Models.BatchNumbers batchNumbers)
        {
            var model = unitOfWork.PatientsRepo.Get(x => x.BatchNumbers.BatchNumber == batchNumbers.BatchNumber);
            ViewBag.batchNumbers = batchNumbers;
            return PartialView("_PatientsGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult PatientsGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))]
            Models.Patients item, [ModelBinder(typeof(DevExpressEditorsBinder))]
            Models.BatchNumbers batchNumbers)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";

            var model = unitOfWork.PatientsRepo.Get(x => x.BatchId == batchNumbers.Id);
            ViewBag.batchNumbers = batchNumbers;
            return PartialView("_PatientsGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult PatientsGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))]
            Models.Patients item, [ModelBinder(typeof(DevExpressEditorsBinder))]
            Models.BatchNumbers batchNumbers)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";

            var model = unitOfWork.PatientsRepo.Get(x => x.BatchId == batchNumbers.Id);
            ViewBag.batchNumbers = batchNumbers;
            return PartialView("_PatientsGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult PatientsGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]
            System.Int32 Id, [ModelBinder(typeof(DevExpressEditorsBinder))]
            Models.BatchNumbers batchNumbers)
        {
            if (Id >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }

            var model = unitOfWork.PatientsRepo.Get(x => x.BatchId == batchNumbers.Id);
            ViewBag.batchNumbers = batchNumbers;
            return PartialView("_PatientsGridViewPartial", model);
        }

        [HttpPost]
        public JsonResult SearchCasePartial([ModelBinder(typeof(DevExpressEditorsBinder))]
            Models.BatchNumbers batchNumbers)
        {
            return Json(new
            {
                //PartialView("_SearchPatientGridViewPartial")
                view = this.RenderViewToString("SearchCasePartial",partial:true)

            }, JsonRequestBehavior.AllowGet);
        }

        /* [HttpPost]
         public PartialViewResult SearchCaseGridPartial([ModelBinder(typeof(DevExpressEditorsBinder))]
             Models.Patients patients)
         {
             var model = unitOfWork.PatientsRepo.Get(x => x.LastName.Contains(patients.LastName)
                                                          &&
                                                          x.FirstName.Contains(patients.FirstName)
                                                          &&
                                                          x.MiddleName.Contains(patients.MiddleName));
             return PartialView(model);
         }*/

        #endregion

        [ValidateInput(false)]
        public PartialViewResult SearchPatientGridViewPartial([ModelBinder(typeof(DevExpressEditorsBinder))]
            Models.Patients patients, [ModelBinder(typeof(DevExpressEditorsBinder))]bool isSearch=false)
        {
            ViewBag.patients = patients;
            var firstName = patients.FirstName ?? "";
            var middleName = patients.MiddleName ?? "";
            var model = unitOfWork.PatientsRepo.Get(x =>
                x.LastName.Contains(patients.LastName) && x.FirstName.Contains(firstName) &&
                x.MiddleName.Contains(middleName));
            ViewBag.counter = model.Count();
            ViewBag.isSearch= isSearch;
            return PartialView("_SearchPatientGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SearchPatientGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))]
            Models.Patients item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";

            return PartialView("_SearchPatientGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SearchPatientGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))]
            Models.Patients item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";

            return PartialView("_SearchPatientGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SearchPatientGridViewPartialDelete(System.Int32 Id)
        {
            var model = new object[0];
            if (Id >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }

            return PartialView("_SearchPatientGridViewPartial", model);
        }
    }
}