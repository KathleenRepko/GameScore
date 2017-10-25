using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GameScore.Models;

namespace GameScore.Controllers
{
    public class ScoresController : Controller
    {
        //field - refers to database
        private GameScoreContext db = new GameScoreContext();

        // GET: Scores
        public ActionResult Index()
        {
            return View(db.Scores.ToList());
            //passes a list of scores entries to the View
        }

        // GET: Scores/Details/5
        public ActionResult Details(int? id) //? makes int parameter optional
        {
            if (id == null) //error-handling code - if no ID is provided, we won't bother searching the database
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);//error message
            }
            Score score = db.Scores.Find(id); //object score in Class Score is created when id is found in Scores model
            if (score == null) //error-handling code - if ID doesn't exist
            { 
                return HttpNotFound(); //error message
            }
            return View(score); //Controller passing the object score to the View
        }

        // GET: Scores/Create - load page to View
        public ActionResult Create()
        {
            return View();
        }

        // POST: Scores/Create - send information from View to Controller
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Points,Team")] Score score) //creates entity to pass to Controller and then to Model
        {
            if (ModelState.IsValid)  //ensures Database is in good condition
            {
                db.Scores.Add(score); 
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(score);
        }

        // GET: Scores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Score score = db.Scores.Find(id);
            if (score == null)
            {
                return HttpNotFound();
            }
            return View(score);
        }

        // POST: Scores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Points,Team")] Score score)
        {
            if (ModelState.IsValid)
            {
                db.Entry(score).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(score);
        }

        // GET: Scores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Score score = db.Scores.Find(id);
            if (score == null)
            {
                return HttpNotFound();
            }
            return View(score);
        }

        // POST: Scores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Score score = db.Scores.Find(id);
            db.Scores.Remove(score);
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
