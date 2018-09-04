using PMIS.Models.Context;
using PMIS.Models.EntityModel;
using PMISUtility.DictionaryFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace PMIS.Controllers
{
    public class PersonController : Controller
    {
        // GET: Person
        public ActionResult Index()
        {
            PMISContext db = new PMISContext();
            var persons = db.Persons.ToList();
            return View(persons);
        }

        public ActionResult Add()
        {
            PMISContext db = new PMISContext();
            var organizations = db.Organizations.Where(x => x.ID != 1).ToList();
            var dictionary = new Dictionary<int, string>();

            foreach (Organization item in organizations)
            {
                dictionary.Add(item.ID, item.Name);
            }

            ViewBag.OrganizationId = dictionary;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Person model)
        {
            try
            {
                PMISContext db = new PMISContext();
                db.Persons.Add(model);
                db.SaveChanges();
                TempData["ADD_STATE"] = "OK";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["ADD_STATE"] = "متاسفانه عملیات با خطا مواجه شد";
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int id)
        {
            PMISContext db = new PMISContext();
            var person = db.Persons.Where(x => x.ID == id).SingleOrDefault();
            if (person != null)
            {
                var dictionary = new Dictionary<int, string>();
                var organization = db.Organizations.Where(x => x.ID == person.OrganizationId).SingleOrDefault();
                var allOrganization = db.Organizations.Where(x => x.ID != person.OrganizationId).ToList();

                dictionary.Add(organization.ID, organization.Name);

                foreach (Organization item in allOrganization)
                {
                    dictionary.Add(item.ID, item.Name);
                }

                ViewBag.OrganizationId = dictionary;
                return View(person);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(Person model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PMISContext db = new PMISContext();
                    var org = db.Persons.Where(x => x.ID == model.ID).SingleOrDefault();
                    db.Entry(org).CurrentValues.SetValues(model);
                    db.SaveChanges();
                    TempData["EDIT_STATE"] = "OK";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception e)
            {
                TempData["EDIT_STATE"] = "FAIL";
                return RedirectToAction("Index");
            }
        }

        public ActionResult Detail(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                PMISContext db = new PMISContext();
                var person = db.Persons.First(o => o.ID == id);
                db.Persons.Remove(person);
                db.SaveChanges();
                return null;
            }
            catch (Exception e)
            {
                return Json("امکان حذف این ساختار وجود ندارد");
            }
        }
    }
}