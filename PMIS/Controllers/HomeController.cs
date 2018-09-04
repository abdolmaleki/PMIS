using PMIS.Models;
using PMIS.Models.Context;
using PMISUtility.Constant;
using System.Web.Mvc;

namespace PMIS.Controllers
{
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {
            if (Session[SessionKey.USER_ID] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }
    }
}