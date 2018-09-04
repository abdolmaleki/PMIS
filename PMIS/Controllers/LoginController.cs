using PMIS.Models;
using PMIS.Models.Context;
using PMIS.Models.EntityModel;
using PMISUtility.Constant;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMIS.Controllers
{
    public class LoginController : BaseController
    {

        public LoginController()
        {
        }
        // GET: Login
        public ActionResult Index()
        {
            return View(new User());
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            if (ModelState.IsValid)
            {
                using (PMISContext dbContext = new PMISContext())
                {
                    var currentUser = dbContext.Users.SingleOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
                    if (currentUser != null)
                    {
                        Session[SessionKey.USER_ID] = currentUser.ID;
                        Session[SessionKey.USER_FullName] = currentUser.FullName;
                        Session[SessionKey.USER_Image] = currentUser.ImagePath;


                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Message = "نام کاربری یا رمز عبور اشتباه است";
                    }
                }
            }
            else
            {
                ViewBag.IsValidModelState = false;
            }
            return View(user);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Index", "Login");
        }
    }
}