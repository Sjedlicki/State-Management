using State_Management_Lab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace State_Management_Lab.Controllers
{
    public class HomeController : Controller
    {
        List<User> storedInfo = new List<User>();

        public ActionResult Index()
        {
            ViewBag.CurrentUser = (User)Session["CurrentUser"];
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult UserInfo(User userData)
        {
            if (Session["CurrentUser"] != null)
            {
                userData = (User)Session["CurrentUser"];
                ViewBag.CurrentUser = userData;
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    storedInfo.Add(userData);
                    Session["CurrentUser"] = userData;
                    return RedirectToAction("DisplayInfo");
                }
                else
                {
                    ViewBag.ErrorMessage = "Registration failed. Try again.";
                    return View("Error");
                }
            }
        }

        public ActionResult DisplayInfo(User u)
        {
            ViewBag.UserInfo = (User)Session["CurrentUser"];
            return View();
        }

        public ActionResult Login(User us)
        {

            ViewBag.User = us;
            return View();
        }

        public ActionResult Logout()
        {
            Session.Remove("CurrentUser");
            return View();
        }

        public ActionResult Confirmed()
        {
            return View();
        }

        public ActionResult ValidLogin(User us)
        {
            foreach (User u in storedInfo)
                if ((us.Email.Contains(u.Email)) && (us.Password.Contains(u.Password)))
                {
                    return RedirectToAction("DisplayInfo");
                }
            return View("DisplayInfo");
        }
    }
}