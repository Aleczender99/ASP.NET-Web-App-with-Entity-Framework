using C__App.DB_Context;
using C__App.DB_Models;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Entity.Controllers
{
    public class HomeController : Controller
    {

        DataContext dataContext = new DataContext("Server=CIAUS\\SQLEXPRESS;Database=entities;Trusted_Connection=True;MultipleActiveResultSets=true");

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public void createTable()
        {
            User user = new User()
            {
                LastName = "Alex",
                FirstName = "Ciau",
                Email = "ciau@google.com",
                DateCreated = DateTime.Now,
            };

            dataContext.Users.Add(user);
            dataContext.SaveChanges();
        }

        public IActionResult List()
        {
            var users = dataContext.Users.ToList();
            return View(users);
        }

        public IActionResult DeleteUser(int UserID)
        {
            var user = dataContext.Users.FirstOrDefault(user => user.ID == UserID);

            if (user != null)
            {
                dataContext.Users.Remove(user);
                dataContext.SaveChanges();
            }

            return RedirectToAction("List");
        }

        public IActionResult Login()
        {
            //createTable(dataContext);

            return View();
        }

        public IActionResult CheckLogin(User loggin)
        {
            var users = dataContext.Users.ToList();

            foreach (User user in users)
            {
                if(user.Email == loggin.Email && user.Password == loggin.Password)
                {
                    return RedirectToAction("List");
                }
            }

            return RedirectToAction("Login");
        }

        public IActionResult NewUser()
        {
            return View();
        }

        public IActionResult AddUser(User users)
        {
            users.DateCreated = DateTime.Now;
            dataContext.Users.Add(users);
            dataContext.SaveChanges();

            return RedirectToAction("Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
