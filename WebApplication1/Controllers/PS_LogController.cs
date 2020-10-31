using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.StoredProcedure;

namespace WebApplication1.Controllers
{
    public class PS_LogController : Controller
    {
        LogAccessLayer logAccess = null;

        public PS_LogController(LogAccessLayer l)
        {
            logAccess = l;
        }
        public IActionResult Index()
        {
            ViewBag.start = "";
            ViewBag.finish = "";
            List<PS_Log> logs = new List<PS_Log>();
            return View(logs);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(DateTime Date_start, DateTime Date_finish)
        {
            ViewBag.Date_start = Date_start;
            ViewBag.Date_finish = Date_finish;
            List<PS_Log> log = logAccess.Get_Log(Date_start, Date_finish);
            return View(log);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public FileResult GetFile(DateTime start, DateTime finish)
        {
            ViewBag.start1 = start;
            ViewBag.finish1 = finish;
            List<PS_Log> lis = logAccess.Get_Log(start, finish);
            Report rep = new Report();
            return rep.GetReportBy(lis);
        }
    }
}
}
