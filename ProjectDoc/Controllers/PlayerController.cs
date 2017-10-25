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
    public class PlayerController : Controller
    {
        private DataContext db = new DataContext();
        

        // GET: Player
        public ActionResult Index()
        {
            return View(db.Players.ToList());
        }

        // GET: Player/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // GET: Player/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Player/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserName,Password,Team,Position,GoalsSum,AssistsSum,InterceptionsSum")] Player player)
        {
            if (ModelState.IsValid)
            {
                if(player.Age<10 || player.Age>35 || db.Players.Any(p=>p.UserName==player.UserName))
                {
                    return View(player);
                }
                db.Players.Add(player);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(player);
        }

        // GET: Player/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Player/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserName,Password,Team,Position,Goals,Assists,Interceptions")] Player player)
        {
            if (ModelState.IsValid)
            {
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(player);
        }

        // GET: Player/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Player/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Player player = db.Players.Find(id);
            db.Players.Remove(player);
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
        public ActionResult AddOrEdit(int id = 0)
        {
            Player p = new Player();
            return View(p);
        }

        [HttpPost]
        public ActionResult AddOrEdit(Player p)
        {
                if (db.Players.Any(m => m.UserName == p.UserName) || p.Age < 10 || p.Age > 35)
            {
                ViewBag.DuplicateMessage = "User Name Already Taken!";
                return RedirectToAction("Index","Home");
            }
            else
            {
                db.Players.Add(p);
                db.SaveChanges();
                ModelState.Clear();
                Session["userName"] = p.UserName;
                Session["ID"] = p.ID.ToString();
                ViewBag.SuccessMessage = Session["userName"];
            }

            return View("Profile", p);
        }

        [HttpGet]
        public ActionResult Login(int id = 0)
        {
            Player p = new Player();

            return View(p);
        }
        [HttpPost]
        public ActionResult Login(Player p)
        {
            if (db.Players.Any(m => m.UserName == p.UserName && m.Password == p.Password))
            {
                Session["userName"] = p.UserName;
                Session["ID"] = p.ID;
                ViewBag.Logged = "Hello " + Session["userName"];
                Player player = db.Players.Where(m => m.UserName == p.UserName).Where(m=> m.Password == p.Password).First();
                return View("Profile",player);
            }

            Session["userName"] = null;
            ViewBag.Message = "Log-In Failed";
            return RedirectToAction("Index","Home");
        }

        public ActionResult Profile(Player p)
        {
            Player player = new Player();

            var pls = db.Players.ToArray();
            for (var i = 0;i <pls.Length;i++)
            {
                if (pls[i].UserName.Equals(Session["userName"].ToString()))
                {
                    player = pls[i];
                    player.updateStats();
                    db.Entry(player).State = EntityState.Modified;
                    db.SaveChanges();
                    pls =pls.OrderBy(pyr => pyr.GoalsSum).ToArray();
                    if (pls.Last().UserName == player.UserName)
                        player.ficici = true;
                    pls = pls.OrderBy(pyr => pyr.AssistsSum).ToArray();
                    if (pls.Last().UserName == player.UserName)
                        player.topChef = true;
                    pls = pls.OrderBy(pyr => pyr.InterceptionsSum).ToArray();
                    if (pls.Last().UserName == player.UserName)
                        player.lastStop= true;
                    
                    return View(player);
                }
            }
            

            return RedirectToAction("Index", "Home");
        }

        public ActionResult About()
        {
            return View();
        }
        /*public ActionResult Profile()
        {
            var id = Session["ID"];
            Player p = db.Players.Find(id);
            if(p !=null)
            {
                return View(p);
            }
            else
            {
                ViewBag.Message = "User Not Logged In";
                return RedirectToAction("Index","Home");
            }
            
        }*/

        public ActionResult LogOut()
        {
            Session["userName"] = null;
            Session["ID"] = null;
            ViewBag.Message = "User Logged Out!";
            return RedirectToAction("Index", "Home");

        }



        public ActionResult Explore()
        {
            return View();
        }



        /*
         * Landing Page ->
         * Log In / Register -->
         * OR --> bad access return to this page 
         * OR --> good access redirect to profile page
         * Profile Page --->
         * OR --> Find DOC page
         * OR --> View personal stats / camps 
         * OR --> About
         * OR --> Log out Btn --> return to Log in / Register
         */
    }
}
