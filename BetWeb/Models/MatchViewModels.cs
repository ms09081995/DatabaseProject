using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace BetWeb.Models
{
    public class MatchViewModels
    {
    }
    public class CurrentMatchesViewModel
    {
        public string FirstTeam { get; set; }

        public string SecondTeam { get; set; }

        public int FirstTeamGoals { get; set; }

        public int SecondTeamGoals { get; set; }

        public DateTime MatchDate { get; set; }

        public string CurrentMinute { get; set; }

        public decimal Bet
        {
            get; set;


        }
    }
}