using MyGigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyGigHub.Controllers
{
    public class ListController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        public ListController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: List
        public ActionResult Index()
        {
            var attendance = _context.Attendances.ToList();
            return View(attendance);
        }
    }
}