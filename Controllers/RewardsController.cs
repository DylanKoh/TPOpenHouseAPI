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
    public class RewardsController : Controller
    {
        private TPOpenHouseEntities db = new TPOpenHouseEntities();

        public RewardsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // POST: Rewards/GetNewReward?rewardName{}&userID={}
        [HttpPost]
        public ActionResult GetNewReward(string rewardName)
        {
            var newReward = (from x in db.Rewards
                             where x.rewardName == rewardName && x.linkedToUser == null
                             select x).FirstOrDefault();
            if (newReward == null)
            {
                return Json("Unable to claim as there are no more available rewards!");
            }
            else
            {
                newReward.linkedToUser = true;
                db.SaveChanges();
                return Json(newReward);
            }
        }

        // POST: Rewards/CheckAvailableRewards?rewardName{}&userID={}
        [HttpPost]
        public ActionResult CheckAvailableRewards(string rewardName, string userID)
        {
            var checkRewardClaim = (from x in db.UserClaims
                                    where x.userIDFK == userID
                                    join y in db.Rewards on x.rewardsIDFK equals y.ID
                                    where y.rewardName == rewardName
                                    select y).FirstOrDefault();
            if (checkRewardClaim == null)
            {
                return Json("No available rewards for this user at the moment!");
            }
            else
            {
                return Json(checkRewardClaim);
            }
        }

        // POST: Rewards/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "ID,rewardName,pointsRequired")] Reward reward)
        {
            if (ModelState.IsValid)
            {
                reward.ID = Guid.NewGuid();
                db.Rewards.Add(reward);
                db.SaveChanges();
                return Json("Created reward!");
            }

            return Json("Unable to create reward!");
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
