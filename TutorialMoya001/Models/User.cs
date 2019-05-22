using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorialMoya001.Models
{
    public class User
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public User()
        {
            
        }

        public User(string email, string name, string lastName, string password)
        {
            this.Email = email;
            this.Name = name;
            this.LastName = lastName;
            this.Password = password;
        }
    }

}
