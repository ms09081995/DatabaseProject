using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetWeb.Models.DataBaseModel;
using BetWebService;
using Newtonsoft.Json;

namespace BetWeb.Controllers
{
    public class MatchController : Controller
    {
        // GET: Match
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Matches(string SelectedLegueId="0", string SelectedTime="0")
        {
            if (SelectedLegueId == "")
                SelectedLegueId = "1";
            if (SelectedTime == "")
                SelectedTime = "1";
            WSToDatabase ws = new WSToDatabase();
            ViewBag.LeaguesList=CreateLeagueSelect(ws);
            ViewBag.Lista = null;
            if (SelectedLegueId != null && SelectedLegueId != "0")
            {
                var l1 = JsonConvert.DeserializeObject<List<Models.DataBaseModel.Match>>(ws.GetMatchesByLeague(Convert.ToInt32(SelectedLegueId), Convert.ToInt32(SelectedTime)));
                ViewBag.Lista = l1;
            }
            ViewBag.SelectedLegueId = SelectedLegueId;
            ViewBag.SelectedTime = SelectedTime;
            return View();
        }
        public ActionResult AddBetForUser(string money, string matchid, string typ)
        {

            WSToDatabase ws = new WSToDatabase();
            if (Session["UserLogin"] != null)
            {
                if (money != "")
                    ws.AddBetForUser(Session["UserLogin"].ToString(), Convert.ToInt32(money), Convert.ToInt32(matchid), Convert.ToInt32(typ));
            }
            return View();
        }

        private List<SelectListItem> CreateLeagueSelect(WSToDatabase ws)
        {
            List<SelectListItem> li = new List<SelectListItem>();
            foreach (Models.DataBaseModel.League l in JsonConvert.DeserializeObject<List<Models.DataBaseModel.League>>(ws.GetLeagues()))
            {
                li.Add(new SelectListItem { Text = l.nazwa, Value = l.id_liga.ToString() });                
            }            
            return li;
        }
        
        public ActionResult GetFirstTeamName( string matchid)
        {
            WSToDatabase ws = new WSToDatabase();
            var match = JsonConvert.DeserializeObject<Models.DataBaseModel.Match>(ws.GetMatcheById(Convert.ToInt32(matchid)));
            return Content(JsonConvert.DeserializeObject<Models.DataBaseModel.Team>(ws.GetTeamById(Convert.ToInt32(match.id_gosp))).nazwa);
                      
        }
        public ActionResult GetSecondTeamName(string matchid)
        {
            WSToDatabase ws = new WSToDatabase();
            var match = JsonConvert.DeserializeObject<Models.DataBaseModel.Match>(ws.GetMatcheById(Convert.ToInt32(matchid)));
            return Content(JsonConvert.DeserializeObject<Models.DataBaseModel.Team>(ws.GetTeamById(Convert.ToInt32(match.id_gosc))).nazwa);
        }

        public ActionResult GetMatchResult(string matchid)
        {
            WSToDatabase ws = new WSToDatabase();
            var match = JsonConvert.DeserializeObject<Models.DataBaseModel.Match>(ws.GetMatcheById(Convert.ToInt32(matchid)));
            return Content(match.bramki_gosp + ":" + match.bramki_gosc); 
         }
        public ActionResult GetMatchMinute(string matchid)
        {
            WSToDatabase ws = new WSToDatabase();
            var match = JsonConvert.DeserializeObject<Models.DataBaseModel.Match>(ws.GetMatcheById(Convert.ToInt32(matchid)));
            return Content(match.minuta.ToString());
        }
        public ActionResult GetMatchBet(string matchid)
        {
            WSToDatabase ws = new WSToDatabase();
            var match = JsonConvert.DeserializeObject<Models.DataBaseModel.Match>(ws.GetMatcheById(Convert.ToInt32(matchid)));
            return Content(match.kurs.ToString());
        }



    }
}