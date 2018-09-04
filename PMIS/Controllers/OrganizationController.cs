using PMIS.Models.Context;
using PMIS.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMIS.Controllers
{
    public class OrganizationController : Controller
    {
        // GET: Organization
        // Error Code Base:12100
        public ActionResult Index()
        {
            try
            {
                PMISContext db = new PMISContext();
                var orgs = db.Organizations.Where(o => !o.SuperOrganizationId.HasValue).ToList();
                return View(orgs);

            }
            catch
            {
                return View("خطای 12101");

            }
        }

        public ActionResult Add(int superId)
        {

            Organization org = new Organization
            {
                SuperOrganizationId = superId
            };
            using (PMISContext db = new PMISContext())
            {
                var superOrgName = db.Organizations.Where(x => x.ID == superId).SingleOrDefault();
                ViewBag.superOrganization = superOrgName.Name;

                var dictionary = new Dictionary<int, string>();

                ViewBag.OrganizationId = dictionary;
                switch (superOrgName.Weight)
                {
                    case 1:        // سازمان صنایع دریایی
                        dictionary.Add(2, "گروه");
                        break;


                    case 2:        // گروه
                        dictionary.Add(3, "صنعت");
                        dictionary.Add(4, "دفتر طراحی / مرکز توسعه");

                        break;

                    case 3:        // صنعت
                        dictionary.Add(4, "دفتر طراحی / مرکز توسعه");
                        break;

                    case 4:        // دفتر طراحی / مرکز توسعه
                        TempData["ADD_STATE"] = "امکان ایجاد ساختار جدید وجود ندارد";
                        return RedirectToAction("Index");
                }

                ViewBag.Weights = dictionary;

            }
            return View(org);
        }

        [HttpPost]
        public ActionResult Add(Organization model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PMISContext db = new PMISContext();
                    db.Organizations.Add(model);
                    db.SaveChanges();
                    TempData["ADD_STATE"] = "OK";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ADD_STATE"] = "فیلدهای خواسته شده را به درستی وارد نکرده اید";
                    return View();
                }
            }
            catch (Exception e)
            {
                TempData["ADD_STATE"] = "متاسفانه عملیات با خطا مواجه شد";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                PMISContext db = new PMISContext();
                var org = db.Organizations.First(o => o.ID == id);
                db.Organizations.Remove(org);
                db.SaveChanges();
                return null;
            }
            catch (Exception e)
            {
                return Json("امکان حذف این ساختار وجود ندارد");
            }
        }

        public ActionResult Edit(int id)
        {
            PMISContext db = new PMISContext();
            var org = db.Organizations.Where(o => o.ID == id).FirstOrDefault();
            return View(org);
        }
        [HttpPost]
        public ActionResult Edit(Organization model)
        {
            try
            {
                PMISContext db = new PMISContext();
                var org = db.Organizations.Where(x => x.ID == model.ID).SingleOrDefault();
                db.Entry(org).CurrentValues.SetValues(model);
                db.SaveChanges();
                TempData["EDIT_STATE"] = "OK";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["EDIT_STATE"] = "FAIL";
                return RedirectToAction("Index");
            }
        }
    }
}