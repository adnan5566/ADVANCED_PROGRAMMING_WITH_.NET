using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssignmentOnZeroHunger.DB;




namespace ZeroHunger.Controllers
{
    public class NGOController : Controller
    {
        // GET: NGO
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

                [HttpPost]
                public ActionResult Login(string Name, string Password)
                {
                    using (var db = new ZeroHunEntities())
                    {

                        var user = db.NGOes.SingleOrDefault(u => u.Name == Name && u.Password == Password);

                        if (user != null)
                        {
                            Session["UserID"] = user.ID;
                            Session["UserID"] = user.Name;
                            return RedirectToAction("Index", "Restaurant");
                        }

                        ViewBag.Error = "Invalid username or password";
                        return View("Login");
                    }
                }


    }
}
