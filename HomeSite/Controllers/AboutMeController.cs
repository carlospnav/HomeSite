using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeSite.Models;
using HomeSiteDomain.Models.About;
using System.Diagnostics;
using HomeSite.Models.Repository;
using System.Net;

namespace HomeSite.Controllers
{
    public class AboutMeController : Controller
    {
        private IAboutMeRepository context;
        // GET: Profile

        public AboutMeController(IAboutMeRepository contextParam)
        {
            this.context = contextParam;
        }

        public ActionResult Index()
        {
            Profile profile = GetMainProfile();
            if (profile == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Oops, we couldn't find the main profile at this time. Try again later!");
            }
            return View(profile);
        }

        public ActionResult GetProfileDetails(int id = 1, string detailsParam = "Proficiencies")
        {
                ViewBag.detailsParam = detailsParam;
                Profile profile;
                using (context) { profile = context.GetSingle(id); }

                if (profile == null)
                {
                    return HttpNotFound();
                }
                ViewBag.gifpath = Server.MapPath("~/Content/Images/AboutMe/LinkIcons/loadgif.gif");
                return PartialView(profile);
        }

        public ActionResult GetDetailsMainSection(string idParam)
        {
            Profile  profile = GetMainProfile();
            string detailName = "";
            string detailDescription = "";
            int detailId = Convert.ToInt32(idParam.Substring(0, 1));
            if (idParam.Contains("interests"))
            {
                Interest interest = profile.Interests.Where(x => x.InterestId == detailId).FirstOrDefault();
                detailName = interest.Name;
                detailDescription = interest.Description;
            }
            else if (idParam.Contains("proficiencies"))
            {
                Proficiency proficiency = profile.Proficiencies.Where(x => x.ProficiencyId == detailId).FirstOrDefault();
                detailName = proficiency.Name;
                detailDescription = proficiency.Description;
            }
            else
                return HttpNotFound();

            string content = "<section id='side-page' class='container' data-toggler='off'><div id='element-description-wrapper'><div id='element-description-container'><div id='element-title-container'><h2 id='element-description-title'>"
                + detailName
                + "</h2></div><div id='element-description-background'></div><div id='element-description-text-background'></div><div id='element-description-text'><p id='element-description-actual-text'>" 
                + detailDescription
                + "</p></div></div></div></section>";

            return Content(content, "text/html");
        }

        public ActionResult GetDetailsSpecificsInfo(string idParam)
        {
            Profile profile = GetMainProfile();
            int detailId = Convert.ToInt32(idParam.Substring(0, 1));
            List<object> jsonList = new List<object>();
            if (idParam.Contains("interests"))
            {
                jsonList.Clear();
                Interest interest = profile.Interests.Where(x => x.InterestId == detailId).FirstOrDefault();
                foreach (InterestDetail item in interest.InterestDetails)
                {
                    jsonList.Add(new { detailName = item.InterestName, detailDesc = item.Description, detailImg = item.ImageUrl });
                }
                return Json(jsonList, JsonRequestBehavior.AllowGet);
            }
            else if (idParam.Contains("proficiencies"))
            {
                jsonList.Clear();
                Proficiency proficiency = profile.Proficiencies.Where(x => x.ProficiencyId == detailId).FirstOrDefault();
                foreach (ProficiencyDetail item in proficiency.ProficiencyDetails)
                {
                    jsonList.Add(new { detailName = item.ProficiencyName, detailDesc = item.Description, detailImg = item.ImageUrl });
                }
                return Json(jsonList, JsonRequestBehavior.AllowGet);
            }
            else
                return HttpNotFound();
        }

        public ActionResult GetResume()
        {
            return File("/Content/Resume/CurriculoCN.docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "CNRESUME.DOCX");
        }

        private Profile GetMainProfile()
        {
            Profile profile;
            using (context) { profile = context.GetSingle(1); }
            return profile;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}