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
                    List<User> TheList = context.Members.ToList();
                    //foreach (User user in TheList)
                    //{
                        //if (user.Email == addUserViewModel.email)
                       // {
                            //ViewBag.error = "That email is already in our system.";
                           // return View();

                        //}

                    //}

                    Hashobject newhash = new Hashobject(addUserViewModel.password);
                    string Hash = newhash.Hashedstring(addUserViewModel.password);

                    User newuser = new User(addUserViewModel.username, addUserViewModel.email, Hash);

                    TheList.Add(newuser);
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
    }

}
