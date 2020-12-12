using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.StoredProcedure;

namespace WebApplication1.Controllers
{
    public class PS_Service_PurposeController : Controller
    {
        PS_Service_PurposeDataAccessLayer _PurposeDataAccessLayer = null;
        PS_CARD_TYPE_DataAccessLayer pS_CARD_TYPE_DataAccessLayer = null;
        public PS_Service_PurposeController(PS_Service_PurposeDataAccessLayer p, PS_CARD_TYPE_DataAccessLayer c)
        {
            _PurposeDataAccessLayer = p;
            pS_CARD_TYPE_DataAccessLayer = c;
        }
        [Authorize]
        public IActionResult Index()
        {
            IEnumerable<PS_Service_Purpose> _Service_Purposes = _PurposeDataAccessLayer.GetAllData();

            return View(_Service_Purposes);
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
            ViewBag.Card = list;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PS_Service_Purpose pS_Service_)
        {
            try
            {
                _PurposeDataAccessLayer.Add(pS_Service_);
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
            PS_Service_Purpose pS_Service_ = _PurposeDataAccessLayer.Details(ID);
            return View(pS_Service_);
        }
        [Authorize]
        public ActionResult Delete(int ID)
        {
            PS_Service_Purpose pS_Service_ = _PurposeDataAccessLayer.Details(ID);

            return View(pS_Service_);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(PS_Service_Purpose pS_Service)
        {
            try
            {
                // TODO: Add delete logic here  
                _PurposeDataAccessLayer.Delete(pS_Service.ID);
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

            PS_Service_Purpose pS = _PurposeDataAccessLayer.Details(ID);

            //ViewData["Card"] = new SelectList(tp, "ID", "Name", Cart.Typeof_Card);
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (PS_CARD_TYPE dr in ct)
            {

                if (pS.Card == dr.NAME)
                {
                    list.Add(new SelectListItem { Text = dr.NAME.ToString(), Value = dr.ID.ToString(), Selected = true });
                }
                else
                {
                    list.Add(new SelectListItem { Text = dr.NAME.ToString(), Value = dr.ID.ToString(), Selected = false });
                }

            }

            ViewBag.Card_type = list;

            return View(pS);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PS_Service_Purpose s_Service_Purpose)
        {
            try
            {
                // TODO: Add update logic here  
                _PurposeDataAccessLayer.Update(s_Service_Purpose);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
