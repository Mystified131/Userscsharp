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

        private readonly ApplicationDbContext context;

        public HomeController(ApplicationDbContext dbContext)
        {
            this.context = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(AddUserViewModel addUserViewModel)
        {
            if (ModelState.IsValid)
            {



                if (addUserViewModel.verify == addUserViewModel.password)
                {

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

                    context.Members.Add(newuser);
                    context.SaveChanges();
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


        public IActionResult Userlist()
        {
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
