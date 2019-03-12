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
        [Route("{year}/{month}", Name = "Month", Order = 0)]
        public async Task<ActionResult> Month(int year, int month) // Month View
        {
            ViewBag.Year = year != 0 ? year : DateTime.Now.Year;
            ViewBag.Month = month != 0 ? month : DateTime.Now.Month;
            DateTime current = new DateTime(year, month, 1);
            IEnumerable<EventViewModel> events = await DocumentDBRepository<EventViewModel>.GetItemsAsync(x => x.Creator.Equals(User.Identity.Name));
            events = events.Where(x => (x.StartDate.Year == current.Year || x.EndDate.Year == current.Year) && (x.StartDate.Month == current.Month  || x.EndDate.Month == current.Month)).ToList();

            return View("Month", events);

        }
        // GET: Calendar/Calendar?year=x&month=y&day=z
        public async Task<ActionResult> Week(int year, int month, int day) // Week view
        {
            ViewBag.Year = year != 0 ? year : DateTime.Now.Year;
            ViewBag.Month = month != 0 ? month : DateTime.Now.Month;
            ViewBag.Day = day != 0 ? day : DateTime.Now.Day;
            DateTime current = new DateTime(year, month, day);
            IEnumerable<EventViewModel> events = await DocumentDBRepository<EventViewModel>.GetItemsAsync(x => x.Creator.Equals(User.Identity.Name));
            events = events.Where(x => (x.StartDate.Year == current.Year || x.EndDate.Year == current.Year) && (x.StartDate.Month == current.Month || x.EndDate.Month == current.Month)).ToList();

            return View("Week", events);

        }

        // GET: Calendar/Day?year=x&month=y&day=z
        public async Task<ActionResult> Day(int year, int month, int day)
        {
            DateTime current = new DateTime(year, month, day);
            IEnumerable<EventViewModel> events = await DocumentDBRepository<EventViewModel>.GetItemsAsync(x => x.Creator.Equals(User.Identity.Name));
            events = events.Where(x => (x.StartDate.Year == current.Year || x.EndDate.Year == current.Year) && (x.StartDate.Month == current.Month || x.EndDate.Month == current.Month) && (x.StartDate.Day == current.Day || x.EndDate.Day == current.Day));

            return View("ViewEvent", events);
        }

        // GET: Calendar/Details/5
        public async Task<ActionResult> Details()
        {
            IEnumerable<EventViewModel> events = await DocumentDBRepository<EventViewModel>.GetItemsAsync( x => x.Creator.Equals(User.Identity.Name));

            return View("ViewEvent", events);
        }

        // POST: Calendar/Create
        [HttpPost]
        public async Task<ActionResult> Create(EventViewModel model)
        {
            try
            {
                ModelState.Remove("StartTime");
                ModelState.Remove("EndTime");
                if (model.StartDate > model.EndDate)
                {
                    Error("StartDate", "The Start Date must be before the End Date.");
                }
                if (
                    model.StartDate.Year == model.EndDate.Year &&
                    model.StartDate.Month == model.EndDate.Month &&
                    model.StartDate.Day == model.EndDate.Day
                   )
                {
                    if (model.StartTime > model.EndTime)
                    {
                        Error("StartTime", "The Start Time must be before the End Time.");
                    }
                    else if (model.StartTime == model.EndTime)
                    {
                        if ((model.StartTime.Hour == 12 && model.StartTime.Minute == 0) && model.IsAllDay == false)
                        {
                            model.IsAllDay = true;
                        }
                    }
                }
                if (model.Frequency == RepeatingFrequency.None)
                {
                    model.IsRepeating = false;
                }

                if (ModelState.IsValid)
                {
                    model.Id = Guid.NewGuid();
                    model.Creator = User.Identity.Name;
                    model.CreationDateTime = DateTime.UtcNow;
                    
                    model.StartDate = model.StartDate.Add(model.StartTime.TimeOfDay);
                    model.EndDate = model.EndDate.Add(model.EndTime.TimeOfDay);

                    await DocumentDBRepository<EventViewModel>.CreateItemAsync(model);

                    return RedirectToAction("Details");
                }
                else
                {
                    return View("CreateEvent");
                }
            }
            catch
            {
                return View("CreateEvent");
            }
        }
        /// <summary>
        /// Adds an error to the model
        /// </summary>
        /// <param name="errorLocation">Model property</param>
        /// <param name="errorMessage">Displayed message in view</param>
        protected virtual void Error(string errorLocation, string errorMessage)
        {
            ModelState.AddModelError(errorLocation, errorMessage);
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
