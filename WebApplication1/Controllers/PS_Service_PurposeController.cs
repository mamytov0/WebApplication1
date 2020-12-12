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
    
}
