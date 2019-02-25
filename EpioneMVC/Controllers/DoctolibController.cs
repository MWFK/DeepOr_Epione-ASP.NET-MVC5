using Domain.Entities;
using EpioneMVC.Models;
using SERVICE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace EpioneMVC.Controllers
{
    public class DoctolibController : Controller
    {
        IDoctolibService service = new DoctolibService();
       
        // GET: Doctolib
        public ActionResult Index(string speciality,string location , string page)
        {
            //pour mettre les dns du modele dans le view model
            List<DoctolibViewModel> list = new List<DoctolibViewModel>();
            foreach (var item in service.getListDoctorsBySpecialityAndLocation(speciality, location, page))
            {
                //ViewBag.userId = _userApplication.GetUserId(HttpContext.User);
                DoctolibViewModel doctolib = new DoctolibViewModel();
                
                doctolib.name = item.name;
                doctolib.img = item.img;
                doctolib.speciality = item.speciality;
                doctolib.address = item.address;
                doctolib.path = item.path;
                list.Add(doctolib);
            }
            if(page==null || page.Equals(""))
            {
                ViewBag.page = 1;

            }else
            {
                int pages = int.Parse(page);
                ViewBag.page = pages;
            }
            if (speciality == null || speciality.Equals(""))
            {
                ViewBag.speciality = "medecin-generaliste";

            }
            else
            {
                ViewBag.speciality = speciality;
            }
            return View(list);
     
        }


        // GET: Doctolib/Details/5
        public ActionResult Details(string path,string adress)
        {
            DoctolibViewModel doctolib = new DoctolibViewModel();
            DoctolibDoctor doctor = service.getDoctorByPath(path);
            doctolib.img = doctor.img;
            doctolib.name = doctor.name;
            doctolib.address= doctor.address ;
            doctolib.lat=doctor.lat;
            doctolib.lng= doctor.lng ;
           doctolib.speciality= doctor.speciality ;
            //doctolib.skills.Add(doctor.skills);
            doctolib.presentationProfession= doctor.presentationProfession ;
            doctolib.Horaires=doctor.horaires;
            doctolib.address = adress.Replace("&#39;", "'") + " "+doctor.address;
            ViewBag.lat= doctor.lat;
            ViewBag.lng = doctor.lng;
            return View(doctolib);
        }

        // GET: Doctolib/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Doctolib/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Doctolib/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Doctolib/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        //Ajax 
        public JsonResult GetSearch(string search)
        {
            var events = new WebClient().DownloadString("https://www.doctolib.fr/api/searchbar/autocomplete.json?search="+ search).Replace("Ã©","é");
            var serializer = new JavaScriptSerializer();

            dynamic obj = serializer.Deserialize(events, typeof(object));
            return new JsonResult { Data = obj, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        
    }
        //Ajax
        public int acceptRequest(int id)
        {

            Doctor doc =service.GetById(id);
            doc.status = 1;
            service.UpdateStatus(doc);
            Session["Accept"] = service.DoctolibAccepted().ToString();

            return 1;
        }
        public int refuseRequest(int id)
        {

            Doctor doc = service.GetById(id);
            doc.status = 2;
            service.UpdateStatus(doc);
            Session["Refused"] = service.DoctolibRefused().ToString();

            return 1;
        }
        // GET: Doctolib/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Doctolib/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult ListRequestRegister()
        {
            IEnumerable<Doctor> list = new List<Doctor>();
            list = service.RequestManagement();
            Session["Request"] = service.DoctolibRequest().ToString();
            return View(list);
        }
        public ActionResult ListAcceptedRegister()
        {
            IEnumerable<Doctor> list = new List<Doctor>();
            list = service.listAcceptedRegister();
            Session["Accept"] = service.DoctolibAccepted().ToString();

            return View(list);
        }
        public ActionResult ListRefusedRegister()
        {
            IEnumerable<Doctor> list = new List<Doctor>();
            list = service.listRefusedRegister();
            Session["Refused"] = service.DoctolibRefused().ToString();

            return View(list);
        }
    }
}
