using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Paid_System_PS_.Models.CallingStoredProcedures;
using Paid_System_PS_.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Paid_System_PS_.Controllers
{
    public class PS_Reletions_PurpController : Controller
    {
        PS_Reletions_PurpDataAccessLayer PS_Reletions_Purp = null;
        PS_Common_Purpose_DataAccessLayer pS_Common_ = null;
        PS_Service_PurposeDataAccessLayer _PurposeDataAccessLayer = null;
        public PS_Reletions_PurpController(PS_Reletions_PurpDataAccessLayer r, PS_Common_Purpose_DataAccessLayer p,PS_Service_PurposeDataAccessLayer s)
        {
            PS_Reletions_Purp = r;
            pS_Common_ = p;
            _PurposeDataAccessLayer = s;
        }
        [Authorize]
        public IActionResult Index()
        {
            IEnumerable<PS_Reletions_Purp> pS_Reletions = PS_Reletions_Purp.GetAllData();
          

            return View(pS_Reletions);
        }
        [Authorize]
        public IActionResult Create()
        {
            IEnumerable<PS_Service_Purpose> sp = _PurposeDataAccessLayer.GetAllData();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (PS_Service_Purpose dr in sp)
            {
                list.Add(new SelectListItem { Text = dr.Description.ToString(), Value = dr.ID.ToString() });
            }
            ViewBag.Service = list;
            IEnumerable<PS_Common_purpose> cp = pS_Common_.GetAllData();
            List<SelectListItem> list1 = new List<SelectListItem>();
            foreach(PS_Common_purpose dr1 in cp)
            {
                list1.Add(new SelectListItem { Text = dr1.Name.ToString(), Value = dr1.ID.ToString() });
            }
            ViewBag.Common = list1;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PS_Reletions_Purp reletions_Purp)
        {
            try
            {
                PS_Reletions_Purp.Add(reletions_Purp);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {
                return View();
            }
        }
        [Authorize]
        public ActionResult Details(int ID)
        {
            PS_Reletions_Purp reletions_Purp = PS_Reletions_Purp.Details(ID);
            return View(reletions_Purp);
        }
        [Authorize]
        public ActionResult Delete(int ID)
        {
            PS_Reletions_Purp reletions_Purp = PS_Reletions_Purp.Details(ID);



            return View(reletions_Purp);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(PS_Reletions_Purp reletions_Purp)
        {
            try
            {
                PS_Reletions_Purp.Delete(reletions_Purp.ID);
                // TODO: Add delete logic here  
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Authorize]
        public ActionResult Edit(int ID)
        {
            PS_Reletions_Purp reletions_Purp = PS_Reletions_Purp.Details(ID);
            IEnumerable<PS_Service_Purpose> sp = _PurposeDataAccessLayer.GetAllData();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (PS_Service_Purpose dr in sp)
            {
                if (reletions_Purp.service_p == dr.Description)
                {
                    list.Add(new SelectListItem { Text = dr.Description.ToString(), Value = dr.ID.ToString(), Selected = true });
                }
                else
                {
                    list.Add(new SelectListItem { Text = dr.Description.ToString(), Value = dr.ID.ToString(), Selected = false });
                }
            }
            ViewBag.Service_Purp = list;
            IEnumerable<PS_Common_purpose> cp = pS_Common_.GetAllData();
            List<SelectListItem> list1 = new List<SelectListItem>();
            foreach (PS_Common_purpose dr1 in cp)
            {
                if (reletions_Purp.common_s == dr1.Name)
                {
                    list1.Add(new SelectListItem { Text = dr1.Name.ToString(), Value = dr1.ID.ToString(), Selected = true });
                }
                else
                {
                    list1.Add(new SelectListItem { Text = dr1.Name.ToString(), Value = dr1.ID.ToString(), Selected = false });
                }
            }
            ViewBag.Common_Serv = list1;

            return View(reletions_Purp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PS_Reletions_Purp _Purp)
        {
            try
            {
                // TODO: Add update logic here  
                PS_Reletions_Purp.Update(_Purp);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}