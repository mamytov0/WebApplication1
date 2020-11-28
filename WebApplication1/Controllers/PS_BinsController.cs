using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Paid_System_PS_.Models;
using Paid_System_PS_.Models.CallingStoredProcedures;

namespace Paid_System_PS_.Controllers
{
    public class PS_BinsController : Controller
    {
        PS_Bins_DataAccessLayer pS_Bins = null;
        PS_CARD_TYPE_DataAccessLayer pS_CARD_TYPE_DataAccessLayer = null;

      
        public PS_BinsController(PS_Bins_DataAccessLayer dal,PS_CARD_TYPE_DataAccessLayer c)
        {
            pS_Bins = dal;
            pS_CARD_TYPE_DataAccessLayer = c;
        }
        [Authorize]
        public IActionResult Index()
        {
            IEnumerable<PS_Bins> bins = pS_Bins.GetAllData();

            return View(bins);
        }
        [Authorize]
        public IActionResult Create()
        {
            IEnumerable<PS_CARD_TYPE> ct = pS_CARD_TYPE_DataAccessLayer.GetAllData();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (PS_CARD_TYPE dr in ct)
            {
                list.Add(new SelectListItem { Text = dr.NAME.ToString(), Value = dr.ID.ToString() });
            }
            ViewBag.Ct = list;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PS_Bins _Bins)
        {
            try
            {
                pS_Bins.Add(_Bins);
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
            PS_Bins bins = pS_Bins.Details(ID);
            return View(bins);
        }
        [Authorize]
        public ActionResult Delete(int ID)
        {
            PS_Bins bins = pS_Bins.Details(ID);
                
            return View(bins);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(PS_Bins bins)
        {
            try
            {
                // TODO: Add delete logic here  
                pS_Bins.Delete(bins.ID);
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
            IEnumerable<PS_CARD_TYPE> ct = pS_CARD_TYPE_DataAccessLayer.GetAllData();

            PS_Bins Bins = pS_Bins.Details(ID);

            List<SelectListItem> list1 = new List<SelectListItem>();
            foreach (PS_CARD_TYPE dr in ct)
            {

                if (Bins.Card_Name == dr.NAME)
                {
                    list1.Add(new SelectListItem { Text = dr.NAME.ToString(), Value = dr.ID.ToString(), Selected = true });
                }
                else
                {
                    list1.Add(new SelectListItem { Text = dr.NAME.ToString(), Value = dr.ID.ToString(), Selected = false });
                }

            }

            ViewBag.Card_Type = list1;

            return View(Bins);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PS_Bins _Bins)
        {
            try
            {
                // TODO: Add update logic here  
                pS_Bins.Update(_Bins);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                //IEnumerable<PS_CARD_TYPE> ct = pS_CARD_TYPE_DataAccessLayer.GetAllData();
                //List<SelectListItem> list1 = new List<SelectListItem>();
                //foreach (PS_CARD_TYPE dr in ct)
                //{

                //    if (_Bins.Card_Name == dr.NAME)
                //    {
                //        list1.Add(new SelectListItem { Text = dr.NAME.ToString(), Value = dr.ID.ToString(), Selected = true });
                //    }
                //    else
                //    {
                //        list1.Add(new SelectListItem { Text = dr.NAME.ToString(), Value = dr.ID.ToString(), Selected = false });
                //    }

                //}

                //ViewBag.Card_Type = list1;
                return View(_Bins);
            }
        }
    }
}