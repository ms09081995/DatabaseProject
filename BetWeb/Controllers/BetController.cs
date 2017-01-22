using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetWebService;
using Newtonsoft.Json;

namespace BetWeb.Controllers
{
    public class BetController : Controller
    {
        // GET: Bet
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MyBets()
        {
            WSToDatabase ws = new WSToDatabase();
            ViewBag.Lista = null;
            var l1 = JsonConvert.DeserializeObject<List<Models.DataBaseModel.Bet>>(ws.GetUserBets("admin"));
            ViewBag.Lista = l1;

            return View();
        }
    }
}