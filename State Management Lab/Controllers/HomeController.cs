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

        List<Items> ItemList = new List<Items>()
        {
           new Items("Hot Chocolate", "Milk, Cocoa, Sugar, Fat", 1.99),
           new Items("Latte",  "Milk, Coffee", 1.99),
           new Items("Coffee",  "Coffee, Water", 1.00),
           new Items("Tea", "Black Tea", 1.00),
           new Items("Frozen Lemonade",  "Lemon, Sugar, Ice", 1.99)
        };

        List<Items> ShoppingCart = new List<Items>();

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
                Session["storedInfo"] = userData;
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    storedInfo.Add(userData);
                    Session["storedInfo"] = storedInfo;
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

        public ActionResult Login()
        {
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
            storedInfo = (List<User>)Session["storedInfo"];

            if (Session["CurrentUser"] == Session["storedInfo"])
                {
                    return View("Error");
                }

                foreach (User u in storedInfo)
                if (u.Email == us.Email && u.Password == us.Password)
                {
                    Session["storedInfo"] = storedInfo;
                    ViewBag.Person = Session["storedInfo"];
                    return RedirectToAction("ReturnUser");
                }
            return View("ReturnUser");
        }

        public ActionResult ReturnUser()
        {
            ViewBag.Person = Session["storedInfo"];
            return View();
        }

        public ActionResult ListItems()
        {
            ViewBag.ItemsList = ItemList;
            return View();
        }

        public ActionResult AddItem(string itemName)
        {
            if(Session["ShoppingCart"] != null)
            {
                ShoppingCart = (List<Items>)Session["ShoppingCart"];
            }

            // Find item in list.
            foreach(Items item in ItemList)
            {
                if(item.ItemName == itemName)
                {
                    ShoppingCart.Add(item);
                }
            }

            Session["ShoppingCart"] = ShoppingCart;
            return RedirectToAction("ListItems");
        }
    }
}