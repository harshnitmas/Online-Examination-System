using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Examination_System.Controllers
{
    public class RegisterController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser(User u)
        {
            if (ModelState.IsValid)
            {
                using (OnlineExaminationDBEntities db = new OnlineExaminationDBEntities())
                {
                    u.UserType="User";
                    db.Users.Add(u);
                    var res=db.SaveChanges();

                    if (res != 0)
                    {
                        ViewData["Result"] = "Success";
                        return View();
                    }
                    else
                    {
                         ViewData["Result"] = "Failed";
                         return View();

                    }
                }
            }
            return View(u);
            
        }
    }
}