using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BetWeb.Models
{
    public class AccountViewModels
    {
    }
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }
        
        
    }
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Potwierdź hasło")]
        public string Password_again { get; set; }

        [Required]
        [Display(Name = "Wiek")]
        public int Age { get; set; }

        //[Required]
        //[Display(Name = "Imię")]
        //public string Name { get; set; }

        //[Required]
        //[Display(Name = "Nazwisko")]
        //public string Surname { get; set; }

        //[Required]
        //[Display(Name = "Pesel")]
        //public string Person_ID { get; set; }

        //[Required]
        //[DataType(DataType.EmailAddress)]
        //[Display(Name = "E-mail")]
        //public string Email_Adress { get; set; }

        //[Required]
        //[DataType(DataType.PhoneNumber)]
        //[Display(Name = "Numer telefonu")]
        //public string Phone_Number { get; set; }
    }

}