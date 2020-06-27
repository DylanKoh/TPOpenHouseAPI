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
    public class UserClaimsController : Controller
    {
        private TPOpenHouseEntities db = new TPOpenHouseEntities();

        public UserClaimsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }


        // POST: UserClaims/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "ID,userIDFK,rewardsIDFK,isClaimed")] UserClaim userClaim)
        {
            if (ModelState.IsValid)
            {
                db.UserClaims.Add(userClaim);
                db.SaveChanges();
                return Json("Claim is online!");
            }

            return Json("Unable to make claim!");
        }

        // POST: UserClaims/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID,userIDFK,rewardsIDFK,isClaimed")] UserClaim userClaim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userClaim).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.rewardsIDFK = new SelectList(db.Rewards, "ID", "rewardName", userClaim.rewardsIDFK);
            ViewBag.userIDFK = new SelectList(db.Users, "userID", "userName", userClaim.userIDFK);
            return View(userClaim);
        }

        // GET: UserClaims/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserClaim userClaim = db.UserClaims.Find(id);
            if (userClaim == null)
            {
                return HttpNotFound();
            }
            return View(userClaim);
        }

        // POST: UserClaims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserClaim userClaim = db.UserClaims.Find(id);
            db.UserClaims.Remove(userClaim);
            db.SaveChanges();
            return RedirectToAction("Index");
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
