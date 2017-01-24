using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetWeb.Models;
using BetWebService;

namespace BetWeb.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        #region Get: Account

        public ActionResult Login()
        {

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login_Success()
        {
            return View();
        }
        public ActionResult GetCurrentUser()
        {
            if (Session["UserLogin"] != null)
            {
                return Content(Session["UserLogin"].ToString());
            }
            else
            {
                return Content("");
            } 
        }
        public ActionResult GetCurrentUserMoney()
        {
            WSToDatabase ws = new WSToDatabase();
            if (Session["UserLogin"] != null)
            {
                
                return Content(ws.GetUserMoney(Session["UserLogin"].ToString()));
            }
            else
            {
                return Content("0");
            }
        }

        public ActionResult SetCurrentUserMoney(string money)
        {
            WSToDatabase ws = new WSToDatabase();
            if (Session["UserLogin"] != null)
            {
                if(money!="")
                    ws.SetUserMoney(Session["UserLogin"].ToString(), Convert.ToInt32(money));
               
                
            }
            return View("Login_Success");
        }

        public ActionResult SendCurrentUserMoney(string money)
        {
            WSToDatabase ws = new WSToDatabase();
            if (Session["UserLogin"] != null)
            {
                if (money != "")
                    ws.SendUserMoney(Session["UserLogin"].ToString(), Convert.ToInt32(money));
                

            }
            return View("Login_Success");

        }
        #endregion

        #region HttpPost Account
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            WSToDatabase ws = new WSToDatabase();            
            if (ModelState.IsValid && ws.Login(model.Login, model.Password)=="true")
            {
                var a = Session["UserLogin"];
                Session["UserLogin"] = model.Login;
                return View("Login_Success");
            }
            else
            {
                return View();
            }
            
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            WSToDatabase ws = new WSToDatabase();
            if (ModelState.IsValid && ws.AddNewUser(model.Login, model.Password,model.Age))
            {
                return View("Login");
            }
            else
            {
                return View();
            }
        }
        #endregion
    }
}