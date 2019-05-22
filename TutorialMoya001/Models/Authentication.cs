using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorialMoya001.Models
{
    public class Authentication
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public Authentication(String Email, string Password)
        {
            this.Email = Email;
            this.Password = Password;
        }

        public bool Login()
        {
            if (Email.Equals("lmartinez.bno@gmail.com") || Email.Equals("karla_rivera@gmail.com"))
                return true;
            return false;
        }
    }
}
