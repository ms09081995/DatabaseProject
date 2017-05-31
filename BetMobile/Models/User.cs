using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BetWeb.Models
{
    public class User
    {
        AccountViewModels _user;
        public User(AccountViewModels user)
        {
            _user = user;
        }
    }
}