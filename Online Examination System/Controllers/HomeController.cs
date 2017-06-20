using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Examination_System.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User u)
        {
            if (ModelState.IsValid)
            {
                using (OnlineExaminationDBEntities db = new OnlineExaminationDBEntities())
                {
                    var checkUser = db.Users.Where(a => a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password)).FirstOrDefault();
                    if (checkUser != null)
                    {
                        Session["UserId"] = checkUser.UserId;
                        Session["UserName"] = checkUser.UserName;
                        if (checkUser.UserType == "Admin")
                        {
                            return RedirectToAction("AdminHomePage");
                        }
                        if (checkUser.UserType == "User")
                        {
                            return RedirectToAction("UserHomePage");
                        }
                        
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "The user name or password is incorrect");
                        return View(u);
                    }
                   
                }
            }
           
            return View(u);
        }
        public ActionResult AdminHomePage()
        {
            if (Session["UserId"] != null)
                return View();
            else
                return RedirectToAction("Login");
        }
        public ActionResult UserHomePage()
        {
            if (Session["UserId"] != null)
                return View();
            else
                return RedirectToAction("Login");
        }

    }
}