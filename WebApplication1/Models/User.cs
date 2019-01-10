using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User(string username, string email, string password)
        {
            Username = username;
            Email = email;
            Password = password;
        }

        public int State (string Loggedin, string Modelvalid)
        {
            int Stateval = 0;
            if(Loggedin == "true")
            {

                Stateval += 1;
            
            }

            if (Modelvalid == "true")
            {

                Stateval += 1;

            }

            return Stateval;

        }
    }
}
