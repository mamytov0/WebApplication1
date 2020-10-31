using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PS_CARD_TYPEController : Controller
    {
        PS_CARD_TYPE_DataAccessLayer pS_CARD_TYPE_DataAccessLayer = null;
        public PS_CARD_TYPEController(PS_CARD_TYPE_DataAccessLayer c)
        {
            pS_CARD_TYPE_DataAccessLayer = c;
        }
        [Authorize]
        public IActionResult Index()
        {
            IEnumerable<PS_CARD_TYPE> card_type = pS_CARD_TYPE_DataAccessLayer.GetAllData();

            return View(card_type);
        }
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PS_CARD_TYPE ps)
        {
            try
            {
                pS_CARD_TYPE_DataAccessLayer.Add(ps);
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
            //ViewData["Doljnost"] = new SelectList(_context.Doljnost, "Id", "Doljnost1", employees.Doljnost);
            PS_CARD_TYPE cARD_TYPE = pS_CARD_TYPE_DataAccessLayer.Details(ID);

            return View(cARD_TYPE);
        }

        [Authorize]
        public ActionResult Edit(int ID)
        {
            PS_CARD_TYPE ps = pS_CARD_TYPE_DataAccessLayer.Details(ID);
            return View(ps);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PS_CARD_TYPE pS_)
        {
            try
            {
                // TODO: Add update logic here  
                pS_CARD_TYPE_DataAccessLayer.Update(pS_);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Authorize]
        public ActionResult Delete(int id)
        {

            PS_CARD_TYPE pS_ = pS_CARD_TYPE_DataAccessLayer.Details(id);

            return View(pS_);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(PS_CARD_TYPE pS_)
        {
            try
            {
                // TODO: Add delete logic here  
                pS_CARD_TYPE_DataAccessLayer.Delete(pS_.ID);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
