using ProjectDoc.DAL;
using ProjectDoc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectDoc.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            Session["ID"] = null;
            Session["userName"] = null;
            Session["campID"] = null;
            Session["admin"] = null;
    
            return View();
        }

        public ActionResult MainPage(Player p)
        {
            if (p!=null)
                return View(p);
            return HttpNotFound();
        } 
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Profile()
        {
            ViewBag.Message = "Your Profile";
            return View();
        }
        public ActionResult Explore()
        {
            ViewBag.Message = "Find DOC";
            return View();
        }

        public ActionResult campPage()
        {
            ViewBag.Message = "Camp Page";
            return View();
        }
    }
}