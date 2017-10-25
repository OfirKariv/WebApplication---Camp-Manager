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
    public class CampController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Camp
        public ActionResult Index()
        {
            return View(db.Camps.ToList());
        }

        public ActionResult Explore()
        {
            return View(db.Camps.ToList());
        }

        [HttpPost]
        public ActionResult Explore(string titleSearch, string descSearch, string locationSearch,string firstPlayerSearch, string secondPlayerSearch)
        {
            
            IEnumerable<Camp> camps = from m in db.Camps select m;
            if(!String.IsNullOrEmpty(locationSearch) && !String.IsNullOrEmpty(descSearch) && !String.IsNullOrEmpty(descSearch) && !String.IsNullOrEmpty(titleSearch) && !String.IsNullOrEmpty(firstPlayerSearch) && !String.IsNullOrEmpty(secondPlayerSearch))
            {
                camps = camps.Where(a=>a.Location.ToString()=="Telaviv").ToList();
            }
            if (!String.IsNullOrEmpty(locationSearch))
            {
                var cmp = camps.GroupBy(a => a.Location);
                foreach (var a in cmp)
                {
                    if (a.Key.ToString() == locationSearch)
                    {
                        camps = a.Select(aa => aa).Where(b => b.Location.ToString() == locationSearch).ToList();
                    }
                }
            }

            if (!String.IsNullOrEmpty(descSearch))
            {
                camps = camps.Where(d => d.Description.Contains(descSearch));
            }
     
            if (!String.IsNullOrEmpty(titleSearch))
            { 
                
                camps = camps.Where(t => t.Name == titleSearch);
            }
     
            if (!String.IsNullOrEmpty(firstPlayerSearch))
            {
                camps = camps.Where(c=>c.Players.Any(a=>a.UserName==firstPlayerSearch));
            }

            if (!String.IsNullOrEmpty(secondPlayerSearch))
            {
                camps = camps.Where(c => c.Players.Any(a => a.UserName == secondPlayerSearch));
            }
                return View(camps);
        }


        // GET: Camp/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Camp camp = db.Camps.Find(id);
            if (camp == null)
            {
                return HttpNotFound();
            }

            Session["campID"] = camp.ID;
            return View(camp);
        }

        // GET: Camp/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Camp/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,NumOfPlayers,sDate,eDate,Location")] Camp camp)
        {
            if (ModelState.IsValid)
            {
                if ((camp.sDate.CompareTo(camp.eDate) < 0)){
                    db.Camps.Add(camp);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(camp);
        }

        // GET: Camp/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Camp camp = db.Camps.Find(id);
            if (camp == null)
            {
                return HttpNotFound();
            }
            Session["campID"] = camp.ID;
            return View(camp);
        }

        // POST: Camp/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,NumOfPlayers,sDate,eDate,Location")] Camp camp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(camp).State = EntityState.Modified;
                db.SaveChanges();
                Session["campID"] = camp.ID;
                return RedirectToAction("Index");
            }
            return View(camp);
        }

        // GET: Camp/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Camp camp = db.Camps.Find(id);
            if (camp == null)
            {
                return HttpNotFound();
            }
            Session["campID"] = camp.ID;
            return View(camp);
        }

        // POST: Camp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Camp camp = db.Camps.Find(id);
            db.Camps.Remove(camp);
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
        public ActionResult CampPage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Camp camp = db.Camps.Find(id);
            if (camp == null)
            {
                return HttpNotFound();
            }

            Session["campID"] = camp.ID;
            return View(camp);

        }


        /*  public ActionResult RegisterPlayer(int? id)
          {
              if (String.IsNullOrEmpty(Session["ID"].ToString()))
              {
                  return RedirectToAction("Index", "Home");
              }
              string playerid = Session["ID"].ToString();

              Player player = null;
              var pls = db.Players.ToArray();
              for (var i = 0;i<pls.Length;i++)
              {
                  if (Session["userName"].ToString().Equals(pls[i].UserName))
                      player = pls[i];
              }

              if (id == null)
              {
                  return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
              }
              Camp camp = db.Camps.Find(id);
              if (camp == null)
                  return new HttpStatusCodeResult(HttpStatusCode.BadRequest);           
              if (player == null)
                  return HttpNotFound();

              int Available = camp.AvailablePlaces();
              bool canRegisterCamp = true;
              if (camp.Players == null)
              {
                  camp.Players = new List<Player>();
              }

                  pls = camp.Players.ToArray();
              for (var j=0;j<pls.Length;j++)
              {
                  if (pls[j].UserName.Equals(player.UserName))
                      canRegisterCamp = false;
              }
             // canRegisterCamp = player.isFreeDate(camp);
           //   canRegisterCamp = player.isExists(camp);


              if (Available > 0 && canRegisterCamp)
              {
                  if (camp.Players == null)
                      camp.Players = new List<Player>();

                  if (player.Camps == null)
                      player.Camps = new List<Camp>();

                  player.Camps.Add(camp);
                  camp.Players.Add(player);


                  db.Entry(camp).State = EntityState.Modified;
                  db.Entry(player).State = EntityState.Modified;
                  db.SaveChanges();
              }


              return RedirectToAction("campPage", camp);

          }

      */
        public ActionResult RegisterPlayer(int? id)
        {
            if (String.IsNullOrEmpty(Session["ID"].ToString()))
            {
                return RedirectToAction("Index", "Home");
            }

            Player player = null;
            var pls = db.Players.ToArray();
            for (var i = 0; i < pls.Length; i++)
            {
                if (Session["userName"].ToString().Equals(pls[i].UserName))
                    player = pls[i];
            }

            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/
            Camp camp = db.Camps.Find(id);
            if (camp == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (player == null)
                return HttpNotFound();

            int Available = camp.AvailablePlaces();
            //    bool canRegisterCamp = true;
            /*     if (camp.Players == null)
                 {
                     camp.Players = new List<Player>();
                 }

                 pls = camp.Players.ToArray();
                 for (var j = 0; j < pls.Length; j++)
                 {
                     if (pls[j].UserName.Equals(player.UserName))
                         canRegisterCamp = false;
                 }*/
            // canRegisterCamp = player.isFreeDate(camp);
            //  canRegisterCamp = player.isExists(camp);


            if (Available > 0 && player.isFreeDate(camp) && player.isExists(camp))
            {
                if (camp.Players == null)
                    camp.Players = new List<Player>();

                if (player.Camps == null)
                    player.Camps = new List<Camp>();

                player.Camps.Add(camp);
                camp.Players.Add(player);

                // for each registration player creates a stats object
                Stats s = new Stats();
                s.CampID = camp.ID;
                s.PlayerID = player.ID;
                s.PlayerAssists = 0;
                s.PlayerGoals= 0;
                s.PlayerInter= 0;


                player.Stats.Add(s);
                db.Stats.Add(s);
                db.Entry(camp).State = EntityState.Modified;
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
            }


            return RedirectToAction("campPage", camp);

        }
        [HttpPost]
        public ActionResult CreateComment(string Text)
        {

            
            Comment comment = new Comment();
            int id = Int32.Parse(Session["campID"].ToString());
            comment.Writer = Session["userName"].ToString();
            comment.CommentText = Text;
            comment.CommentDate = DateTime.Now;
            
            Camp camp = db.Camps.Find(id);
            if (camp == null)
            {
                return HttpNotFound();
            }


            if (camp.Comments == null)
                camp.Comments = new List<Comment>();

            db.Comments.Add(comment);
            camp.Comments.Add(comment);
            db.Entry(camp).State = EntityState.Modified;
            db.SaveChanges();


            return View("CampPage", camp);
        }

    }
}
