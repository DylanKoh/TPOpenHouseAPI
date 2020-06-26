using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TPOpenHouseAPI;

namespace TPOpenHouseAPI.Controllers
{
    public class EventsController : Controller
    {
        private TPOpenHouseEntities db = new TPOpenHouseEntities();

        public EventsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // POST: Events
        [HttpPost]
        public ActionResult Index()
        {
            return Json(db.Events.ToList());
        }

        // POST: Events/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "ID,eventName,eventVenue,eventTime")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                return Json("Event created successuflly");
            }

            return Json("Unable to create event! Please try again later!");
        }

        // POST: Events/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID,eventName,eventVenue,eventTime")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return Json("Event edited successfully!");
            }
            return Json("Unable to edit event! Please try again later!");
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
            db.SaveChanges();
            return Json("Event deleted successfully!");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
