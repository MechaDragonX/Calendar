using Calendar.Models;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Calendar.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public CalendarController()
        {
        }

        public CalendarController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Calendar
        public ActionResult CreateEvent()
        {
            return View();
        }

        // GET: Calendar/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Calendar/Details/5
        public ActionResult Details(List<EventViewModel> events)
        {
            events = new List<EventViewModel>();
            events.Add(new EventViewModel { Description = "sdasd", Name = "dfsdfsdfdsf" });
            return View("ViewEvent", events);
        }
        // GET: Calendar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Calendar/Create
        [HttpPost]
        public async Task<ActionResult> Create(EventViewModel model)
        {
            try
            {
                // TODO: Add insert logic here

//                 //open file stream
//                 using (StreamWriter file = System.IO.File.CreateText(@"Calendar\Data\"))
//                 {
//                     JsonSerializer serializer = new JsonSerializer();
//                     //serialize object directly into file stream
//                     serializer.Serialize(file, model);
//                 }

                return RedirectToAction("Details", new List<EventViewModel> { model });
            }
            catch
            {
                return View();
            }
        }

        // GET: Calendar/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Calendar/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Calendar/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Calendar/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
