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

        // POST: UserClaims/GetClaim?rewardID={}
        [HttpPost]
        public ActionResult GetClaim(Guid rewardID)
        {
            var RewardClaim = db.UserClaims.Where(x => x.rewardsIDFK == rewardID).Select(x => x).FirstOrDefault();
            return Json(RewardClaim);
        }

        // POST: UserClaims/CheckRedemptionStatus?rewardID={}
        [HttpPost]
        public ActionResult CheckRedemptionStatus(Guid rewardID)
        {
            var checkStatus = db.UserClaims.Where(x => x.rewardsIDFK == rewardID).Select(x => x.isClaimed).FirstOrDefault();
            if (checkStatus)
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
                
        }

        // POST: UserClaims/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID,userIDFK,rewardsIDFK,isClaimed")] UserClaim userClaim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userClaim).State = EntityState.Modified;
                db.SaveChanges();
                return Json("Edit claim successful!");
            }
            return Json("Unable to edit claim!");
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
