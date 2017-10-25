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
    
    public class AdminController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Admin
        public ActionResult Index()
        {
            return View(db.Admin.ToList());
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admin.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Admin admin)
        {
            admin.ConfirmPassword = admin.Password;
            //if (ModelState.IsValid)
            //{
            if (!db.Admin.Any(a => a.UserName == admin.UserName))
            {
                Session["admin"] = admin.UserName;
                db.Admin.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index","Camp");
            }
            //}

            return View(admin);
        }


        
        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admin.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserName,Password")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admin.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.Admin.Find(id);
            db.Admin.Remove(admin);
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

        [HttpGet]
        public ActionResult Login()
        {
            Admin admin = new Admin();

            return View(admin);
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            if (admin == null)
            {
                ViewBag.Message = "Admin Log In Failed";
                return RedirectToAction("Index", "Home");
            }
            var admins = db.Admin.ToArray();
            if (admins == null)
            {
                ViewBag.Message = "Admin Log In Failed";
                return RedirectToAction("Index", "Home");
            }
            if(db.Admin.Any(m => m.UserName == admin.UserName && m.Password == admin.Password))
            {  
                Session["ID"] = admin.ID;
                Session["admin"] = admin.UserName;
                return RedirectToAction("Index", "Camp");
            }
            else
            { 
                Session["ID"] = null;
                Session["userName"] = null;
            }
            return RedirectToAction("Index","Home");
        }
    }
}
