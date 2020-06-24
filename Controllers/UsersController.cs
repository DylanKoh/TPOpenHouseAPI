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
    public class UsersController : Controller
    {
        private TPOpenHouseEntities db = new TPOpenHouseEntities();

        public UsersController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // POST: Users
        [HttpPost]
        public ActionResult Index()
        {
            return Json(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        // POST: Users/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "userID,userName,password,Points")] User user)
        {
            if (ModelState.IsValid)
            {
                var findUser = db.Users.Where(x => x.userID == user.userID).Select(x => x).FirstOrDefault();
                if (findUser != null)
                {
                    return Json("User ID has been used!");
                }
                else
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return Json("User created successfully!");
                }
                
            }
            return Json("Unable to create user account! Please contact our administrator!");

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
