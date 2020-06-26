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
    public class QuestionsController : Controller
    {
        private TPOpenHouseEntities db = new TPOpenHouseEntities();

        public QuestionsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        [HttpPost]
        // POST: Questions
        public ActionResult Index()
        {
            return Json(db.Questions.ToList());
        }

        [HttpPost]
        // POST: Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return Json("Unable to find question!");
            }
            return Json(question);
        }


        // POST: Questions/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "ID,AnswerOne,AnswerTwo,AnswerThree,AnswerFour,Correct,questionCategory,questionString")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();
                return Json("Question created successfuly!");
            }

            return Json("Unable to create question! Please contact our administrator!");
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
