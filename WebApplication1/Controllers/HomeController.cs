using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public static string Loggedin;
        public static string Modelvalid;
        public static User currentuser = new User("username", "email", "password");

        private readonly ApplicationDbContext context;

        public HomeController(ApplicationDbContext dbContext)
        {
            this.context = dbContext;
        }

        public IActionResult Index()
        {
            //User currentuser = new User("username", "email", "password");
            int currentstate = currentuser.State(Loggedin, "");

            if(currentstate == 1)
            {
                ViewBag.session = "true";

            }

            else
            {
                ViewBag.session = "false";

            }
            return View();
        }

        public IActionResult AddUser()
        {
            //User currentuser = new User("username", "email", "password");
            int currentstate = currentuser.State(Loggedin, "");

        if(currentstate == 1)
            {

                return Redirect("/");
            }

            else
            {
                
                return View();

            }
        }

        [HttpPost]
        public IActionResult AddUser(AddUserViewModel addUserViewModel)
        {
           

            if (ModelState.IsValid)
            {



                if (addUserViewModel.verify == addUserViewModel.password)
                {
                    int pwlen = addUserViewModel.password.Length;
                    
                    if (pwlen < 9)


                    {

                        ViewBag.error = "The password must be longer than 8 characters.";
                        return View();

                    }

                    String pwtest = addUserViewModel.password;

                    var withoutSpecial = new string(pwtest.Where(c => Char.IsLetterOrDigit(c)
                                            || Char.IsWhiteSpace(c)).ToArray());


                    if (pwtest.Any(char.IsUpper) &&
                        pwtest.Any(char.IsLower) &&
                        pwtest.Any(char.IsDigit) &&
                        pwtest != withoutSpecial)

          
                    {
                        //password clears complexity test
                    }

                    else

                    {
                        ViewBag.error = "Your password must contain upper and lowercase letters, numbers and special characters.";
                        return View();

                    }



                    List<User> matches = context.Members.Where(c => c.Email == addUserViewModel.email).ToList();

                    if (matches.Count > 0)

                    {

                        ViewBag.error = "That email is already in our system.";
                        return View();

                    }

                    string passobj = "cheese" + addUserViewModel.password;

                    Hashobject newhash = new Hashobject(passobj);
                    string Hash = newhash.Hashedstring(passobj);

                    User newuser = new User(addUserViewModel.username, addUserViewModel.email, Hash);

                    currentuser = newuser;
                    context.Members.Add(newuser);
                    context.SaveChanges();
                    Loggedin = "true";
                    return Redirect("/Home/Registered");

                }

                else
                {

                    return View();
                }
            }

            else
            {

                return View();
            }
        }

        public IActionResult Registered()
        {
            return View();
        }

        public IActionResult Logout()
        {
            Loggedin = "";
            return Redirect("/");
        }


        public IActionResult Userlist()
       {
            if(Loggedin != "true")
            {

                return Redirect("/");

            }

            List<User> TheList = context.Members.ToList();

            ViewBag.userslist = TheList;

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                List<User> matches1 = context.Members.Where(c => c.Username == loginViewModel.username).ToList();

                string passobj = "cheese" + loginViewModel.password;

                Hashobject newhash = new Hashobject(passobj);
                string Hash = newhash.Hashedstring(passobj);

                List<User> matches2 = matches1.Where(c => c.Password == Hash).ToList();

                if (matches2.Count == 1)
                {
                    User logusr = matches1.Single(c => c.Password == Hash);
                    currentuser = logusr;
                    Loggedin = "true";
                    return Redirect("/Home/LoggedIn");
                }

                else
                {
                    ViewBag.error = "No such user found in database. Feel free to register.";
                    return View();
                }
               

                }

            else
            {

                return View();

            }

        }

        public IActionResult LoggedIn()
        {
            return View();
        }
    }

}
