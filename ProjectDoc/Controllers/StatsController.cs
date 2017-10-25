using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectDoc.DAL;
using ProjectDoc.Models;

namespace ProjectDoc.Controllers
{
    public class StatsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Stats
        public ActionResult Index()
        {
            return View(db.Stats.ToList());
        }

        // GET: Stats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stats stats = db.Stats.Find(id);
            if (stats == null)
            {
                return HttpNotFound();
            }
            return View(stats);
        }

        // GET: Stats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CampID,PlayerID,PlayerGoals,PlayerAssists,PlayerInter")] Stats stats)
        {
            if (ModelState.IsValid)
            {
                db.Stats.Add(stats);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stats);
        }

        // GET: Stats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stats stats = db.Stats.Find(id);
            if (stats == null)
            {
                return HttpNotFound();
            }
            return View(stats);
        }

        // POST: Stats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CampID,PlayerID,PlayerGoals,PlayerAssists,PlayerInter")] Stats stats)
        {
            if (ModelState.IsValid)
            {
                if(stats.PlayerAssists<0 || stats.PlayerInter < 0 || stats.PlayerGoals < 0)
                {
                    return View(stats);
                }
                db.Entry(stats).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stats);
        }

        // GET: Stats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stats stats = db.Stats.Find(id);
            if (stats == null)
            {
                return HttpNotFound();
            }
            return View(stats);
        }

        // POST: Stats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stats stats = db.Stats.Find(id);
            db.Stats.Remove(stats);
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
