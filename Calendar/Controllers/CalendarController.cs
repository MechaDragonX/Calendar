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
using Calendar.Repository;

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

        // GET: Calendar/CreateEvent
        public ActionResult CreateEvent()
        {
            return View();
        }

     

        // GET: Calendar/Calendar?year=x&month=y
        public ActionResult Calendar( int year, int month)
        {
            if (year == 0 && month == 0)
            {
                ViewBag.Year = DateTime.Now.Year;
                ViewBag.Month = DateTime.Now.Month;
            }
            else
            {
                ViewBag.Year = year;
                ViewBag.Month = month;
            }
            return View();
        }

        // GET: Calendar/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Calendar/Details/5
        public async Task<ActionResult> Details()
        {
            IEnumerable<EventViewModel> events = await DocumentDBRepository<EventViewModel>.GetItemsAsync(x => x.Creator.Equals(User.Identity.Name));

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
                model.Id = Guid.NewGuid();
                model.Creator = User.Identity.Name;
                model.CreationDateTime = DateTime.UtcNow;

                model.StartDate = model.StartDate.Add(model.StartTime.TimeOfDay);
                model.EndDate = model.EndDate.Add(model.EndTime.TimeOfDay);

                await DocumentDBRepository<EventViewModel>.CreateItemAsync(model);

                return RedirectToAction("Details");
            }
            catch
            {
                return View("CreateEvent");
            }
        }

        // GET: Calendar/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            EventViewModel item = await DocumentDBRepository<EventViewModel>.GetItemAsync(id);
            item.StartTime = item.StartTime.Add(item.StartDate.TimeOfDay);
            item.EndTime =  item.EndTime.Add(item.EndDate.TimeOfDay);
            return View("EditEvent", item);
        }

        // POST: Calendar/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, EventViewModel model)
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
        public async Task<ActionResult> Delete(Guid id)
        {
            await DocumentDBRepository<EventViewModel>.DeleteItemAsync(id);

            return RedirectToAction("Details");
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
