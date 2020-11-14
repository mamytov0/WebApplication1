using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Paid_System_PS_.Models;
using Paid_System_PS_.Models.CallingStoredProcedures;

namespace Paid_System_PS_.Controllers
{
    public class PS_Common_purposeController : Controller
    {
        PS_Common_Purpose_DataAccessLayer pS_Common_ = null;
        Type_CardDataAccessLayer type_Card = null;
        public PS_Common_purposeController(PS_Common_Purpose_DataAccessLayer p,Type_CardDataAccessLayer t)
        {
            pS_Common_ = p;
            type_Card = t;
        }
        [Authorize]
        public IActionResult Index()
        {
            IEnumerable<PS_Common_purpose> _Common_Purposes = pS_Common_.GetAllData();


            return View(_Common_Purposes);
        }
        [Authorize]
        public IActionResult Create()
        {
            IEnumerable<Type_Card> tp = type_Card.GetAllData();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach(Type_Card dr in tp)
            {
                list.Add(new SelectListItem { Text = dr.Name.ToString(), Value = dr.ID.ToString() });
            }
            ViewBag.Card = list;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PS_Common_purpose common_Purpose)
        {
            try
            {
                pS_Common_.Add(common_Purpose);
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
            PS_Common_purpose _Purpose = pS_Common_.Details(ID);
            return View(_Purpose);
        }

        [Authorize]
        public ActionResult Delete(int ID)
        {
            PS_Common_purpose _Purpose = pS_Common_.Details(ID);

            return View(_Purpose);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(PS_Common_purpose _Purpose)
        {
            try
            {
                // TODO: Add delete logic here  
                pS_Common_.Delete(_Purpose.ID);
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
            IEnumerable<Type_Card> tp = type_Card.GetAllData();
            //var Cart = pS_Common_.GetAllData().FirstOrDefault(p => p.ID == ID);
            PS_Common_purpose CP = pS_Common_.Details(ID);
            
            //ViewData["Card"] = new SelectList(tp, "ID", "Name", Cart.Typeof_Card);
            List<SelectListItem> list = new List<SelectListItem>();
            
            foreach (Type_Card dr in tp)
            {

                if (CP.Card == dr.Name)
                {
                    list.Add(new SelectListItem { Text = dr.Name.ToString(), Value = dr.ID.ToString(), Selected = true });
                }
                else
                {
                    list.Add(new SelectListItem { Text = dr.Name.ToString(), Value = dr.ID.ToString(), Selected = false });
                }

            }

            ViewBag.Typeof_Card = list;

            return View(CP);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PS_Common_purpose pS_)
        {
            try
            {
                // TODO: Add update logic here  
                pS_Common_.Update(pS_);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}