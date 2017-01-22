using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace BetWebService
{
    
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    
    
    public class WSToDatabase : System.Web.Services.WebService
    {
        private STS2Entities context;

        public WSToDatabase() : base()
        {
            context = new STS2Entities();
            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ProxyCreationEnabled = false;
        }

        #region Account WebMethod

        [WebMethod]
        public string Login(string login, string haslo)
        {
            foreach (User u in context.Users)
            {
                if (u.login == login && u.haslo == haslo)
                {
                    return JsonConvert.SerializeObject(true);
                }

            }
            return JsonConvert.SerializeObject(false);
        }
        [WebMethod]
        public bool AddNewUser(string login, string haslo, int wiek)
        {
            if (GetUserIndex(login) > -1)
                return false;
          
            User newuser = new User();
            newuser.login = login;
            newuser.haslo = haslo;
            newuser.wiek = wiek;
            newuser.stan_konta = 0;           

            context.Users.Add(newuser);
            context.SaveChanges();
            return true;
        }

        [WebMethod]
        public bool DeleteUser(string login)
        {
            User user = GetUser(login);
            if (user == null)
                return false;

            context.Users.Remove(user);
            context.SaveChanges();
            return true;
        }
        #endregion

        #region GetALl WebMethod

        [WebMethod]
        public string GetUsers()
        {
            
            List<User> li = new List<User>(); 
            foreach( User u in context.Users)
            {
                li.Add(u);
            }

            return JsonConvert.SerializeObject(li, Formatting.Indented);
        }
        [WebMethod]
        public string GetTeams()
        {
            List<Match> li = new List<Match>();

            foreach (Match m in context.Matches)
            {
                li.Add(m);
            }

            return JsonConvert.SerializeObject(li, Formatting.Indented);
        }
        [WebMethod]
        public string GetBets()
        {
            List<Bet> li = new List<Bet>();

            foreach (Bet z in context.Bets)
            {
                li.Add(z);
            }

            return JsonConvert.SerializeObject(li, Formatting.Indented);
        }
        [WebMethod]
        public string GetMatches()
        {
            List<Match> li = new List<Match>();

            foreach (Match m in context.Matches)
            {
                li.Add(m);
            }

            return JsonConvert.SerializeObject(li, Formatting.Indented);
        }
        [WebMethod]
        public string GetMatchesByLeague(int leagueID, int time =0)
        {
            if (leagueID == 0)
                return null;
            List<Match> li = new List<Match>();

            foreach (Match m in context.Matches)
            {
                if ((time == 1 && m.minuta == 0) || (time == 0 && m.minuta < 90&& m.minuta>0) || (time == 2 && m.minuta > 90))
                {
                    if (leagueID == GetMatchLeague(Convert.ToInt32(m.id_gosp)).id_liga)
                        li.Add(m);
                }
            }
            return JsonConvert.SerializeObject(li, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
        [WebMethod]
        public string GetLeagues()
        {
            List<League> li = new List<League>();

            foreach (League l in context.Leagues)
            {
                li.Add(l);
            }

            return JsonConvert.SerializeObject(li, Formatting.Indented);
        }
        [WebMethod]
        public string GetUserMoney(string login)
        {
            var u = GetUser(login);            
            if (u==null)
                return null;
            UpdateUserMoney(u);
            return u.stan_konta.ToString();

        }
        [WebMethod]
        public void SetUserMoney(string login, int money)
        {
            var u = GetUser(login);

            if (u == null)
                return;
            u.stan_konta += money;           
        }
        [WebMethod]
        public void SendUserMoney(string login, int money)
        {
            var u = GetUser(login);

            if (u == null)
                return;
            if(u.stan_konta>money)
                u.stan_konta -= money;
        }

        [WebMethod]
        public string GetUserBets(string login)
        {
            var u = GetUser(login);

            if (u == null)
                return null;
            else
            {
                var li = GetUserBets(u);
                return JsonConvert.SerializeObject(li, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }
           
        }
        [WebMethod]
        public string GetUserBetsByStatus(string login, string status)
        {
            var u = GetUser(login);

            if (u == null)
                return null;
            else
            {
                var li = GetUserBets(u);
                foreach(Bet b in li)
                {
                    if(b.status!=status)
                    {
                        li.Remove(b);
                    }
                }
                return JsonConvert.SerializeObject(li, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }
        }

        [WebMethod] 
        public bool AddBetForUser(string login, int money, int matchid, int teamid)
        {
            var u = GetUser(login);
            if (u == null)
                return false;
            if(u.stan_konta<money)
            {
                return false;
            }
            var match = GetMatchById(matchid);
            if(match==null)
            {
                return false;
            }
            var winsum = money * match.kurs;
            Bet newbet = new Bet();
            newbet.id_mecz = match.id_mecz;
            newbet.id_user = u.id_user;
            newbet.kwota = money;
            newbet.status = "W toku";
            newbet.Match = match;
            newbet.User = u;
            newbet.wygrana = winsum;

            context.Bets.Add(newbet);
            context.SaveChanges();

            return true;               

        }

        [WebMethod]
        public void UpdateMatchMinute()
        {
            foreach(Match m in context.Matches)
            {
                if(m.minuta<90)
                {
                    m.minuta += 1;
                }
            }
            context.SaveChanges();

        }
        [WebMethod]
        public  string GetTeamById(int teamid)
        {
            return JsonConvert.SerializeObject(context.Teams.SingleOrDefault(team => team.id_druz == teamid), Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            
        }
        [WebMethod]
        public string GetMatcheById(int matchid)
        {
            return JsonConvert.SerializeObject(context.Matches.SingleOrDefault(match => match.id_mecz == matchid), Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

        }


        #endregion

        #region Private Method

        private User GetUser(string login)
        {
            return context.Users.SingleOrDefault(user => user.login == login);
        }

        private int GetUserIndex(string login)
        {
            if (context.Users.SingleOrDefault(user => user.login == login) == null)
                return -1;
            return context.Users.SingleOrDefault(user => user.login == login).id_user;
        }
        private Match GetMatchById(int matchid)
        {
            return context.Matches.SingleOrDefault(match => match.id_mecz == matchid);
        }
        private League GetMatchLeague(int TeamId)
        {
            var leagueId = context.Teams.SingleOrDefault(t => t.id_druz == TeamId).id_liga;
            return context.Leagues.SingleOrDefault(league => league.id_liga == leagueId);
        }
        private void UpdateUserMoney(User user)
        {
            var bets = GetUserBets(user);
            if (bets != null)
            {
                foreach (Bet b in bets)
                {
                    b.status = "Do wyplaty";
                    user.stan_konta += b.wygrana;
                    b.status = "Wyplacony";
                }
            }
            context.SaveChanges();
        }

        private List<Bet> GetUserBets(User user)
        {
            List<Bet> bets = new List<Bet>();            
            foreach(Bet b in context.Bets)
            {
                if(b.id_user==user.id_user)
                {
                    bets.Add(b);
                }
            }
            return bets;
        }
        #endregion

    }
}
