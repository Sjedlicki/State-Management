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
           new Items("Hot Chocolate", "Milk, Cocoa, Sugar, Fat", 2.49),
           new Items("Latte",  "Milk, Coffee", 1.99),
           new Items("Coffee",  "Coffee, Water", 0.99),
           new Items("Tea", "Black Tea", 1.25),
           new Items("Frozen Lemonade",  "Lemon, Sugar, Ice", 2.99)
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
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if (Session["storedInfo"] != null)
                    {
                        storedInfo = (List<User>)Session["storedInfo"];
                    }
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
            if (Session["storedInfo"] != null)
            {
                storedInfo = (List<User>)Session["storedInfo"];
            }
            else
            {
                return View("Error");
            }

                foreach (User u in storedInfo)
                if (u.Email == us.Email && u.Password == us.Password)
                {
                    Session["CurrentUser"] = u;
                    return RedirectToAction("ReturnUser");
                }
            return View("ReturnUser");
        }

        public ActionResult ReturnUser()
        {
            ViewBag.Person = Session["CurrentUser"];
            return View();
        }

        public ActionResult ListItems()
        {
            if (Session["CurrentUser"] == null)
            {
                return View("Error");
            }
            else
            {
                ViewBag.ItemsList = ItemList;
                return View();
            }
        }

        public ActionResult AddItem(string itemName)
        {
                if (Session["ShoppingCart"] != null)
                {
                    ShoppingCart = (List<Items>)Session["ShoppingCart"];
                }

                // Find item in list.
                foreach (Items item in ItemList)
                {
                    if (item.ItemName == itemName)
                    {
                        ShoppingCart.Add(item);
                    }
                }

                Session["ShoppingCart"] = ShoppingCart;
                return RedirectToAction("ListItems");
        }

        public ActionResult RemoveItem(string itemName)
        {
            if (Session["ShoppingCart"] != null)
            {
                ShoppingCart = (List<Items>)Session["ShoppingCart"];
            }

            foreach (Items item in ShoppingCart)
            {
                if (itemName == item.ItemName)
                {
                    ShoppingCart.Remove(item);
                    Session["ShoppingCart"] = ShoppingCart;
                    break;
                }
            }
            ViewBag.Cart = Session["ShoppingCart"];
            return RedirectToAction("Cart");
        }

        public ActionResult Cart()
        {
            ViewBag.Cart = Session["ShoppingCart"];
            return View();
        }
    }
}