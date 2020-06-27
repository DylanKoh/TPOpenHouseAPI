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
    public class UserResponsesController : Controller
    {
        private TPOpenHouseEntities db = new TPOpenHouseEntities();

        public UserResponsesController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // POST: UserResponses
        [HttpPost]
        public ActionResult Index()
        {
            return Json(db.UserResponses.ToList());
        }


        // POST: UserResponses/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "ID,userIDFK,questionIDFK,userAnswer,isCorrect")] UserResponse userResponse)
        {
            if (ModelState.IsValid)
            {
                db.UserResponses.Add(userResponse);
                db.SaveChanges();
                if (userResponse.isCorrect)
                {
                    var getUser = db.Users.Where(x => x.userID == userResponse.userIDFK).Select(x => x).FirstOrDefault();
                    getUser.points += 10;
                    db.SaveChanges();
                    return Json("Congrats! Your answer was correct!");
                }
                else
                {
                    return Json("Your answer is incorrect!");
                }
                
            }
            return Json("Unable to submit response! Please try again later!");
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
