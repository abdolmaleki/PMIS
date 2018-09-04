using PMIS.Models;
using PMIS.Models.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMIS.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            PMISContext db = new PMISContext();
            var users = db.Users.ToList();
            return View(users);
        }

        public ActionResult Add()
        {
            PMISContext db = new PMISContext();
            var persons = db.Persons.ToList();
            var dictionary = new Dictionary<int, string>();

            foreach (Models.EntityModel.Person item in persons)
            {
                dictionary.Add(item.ID, item.FirstName + " " + item.LastName);
            }

            ViewBag.PersonIds = dictionary;
            return View();
        }

        [HttpPost]
        public ActionResult Add(User model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                    string fileExtention = Path.GetExtension(model.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + fileExtention;
                    model.ImagePath = "~/Images/ProfileImages/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Images/ProfileImages/"), fileName);
                    model.ImageFile.SaveAs(fileName);


                    PMISContext db = new PMISContext();
                    db.Users.Add(model);
                    db.SaveChanges();
                    TempData["ADD_STATE"] = "OK";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                TempData["ADD_STATE"] = "متاسفانه عملیات با خطا مواجه شد";
                return RedirectToAction("Index");
            }

            return View();
        }

    }
}