using SERVICE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EpioneMVC.Controllers
{
    public class HomeController : Controller
    {
        INbr_Treated_Patients_Service X = new Nbr_Treated_Patients_Service();
        ICancellation_Rate_Service Y = new Cancellation_Rate_Service();
        IUsed_Timeslot_Rate_Service Z = new Used_Timeslot_Rate_Service();

        // GET: AppointmentMK
        public ActionResult Index()
        {
            /*Nbr_Treated_Patients*/
            ViewBag.Day = X.Count_Nbr_Treated_Patients_Per_Day();
            ViewBag.Week = X.Count_Nbr_Treated_Patients_Per_Week();
            ViewBag.Month = X.Count_Nbr_Treated_Patients_Per_Month();

            /*Cancellation_Rate*/
            ViewBag.Day_Cancellation = ((Y.Cancellation_Rate_Per_Day()) * 100).ToString("0"); //ToString("0") so the float does not take the decimals
            ViewBag.Week_Cancellation = ((Y.Cancellation_Rate_Per_Week()) * 100).ToString("0");
            ViewBag.Month_Cancellation = ((Y.Cancellation_Rate_Per_Month()) * 100).ToString("0");

            /*Timeslot_Using_Rate*/
            ViewBag.Day_Timeslot = ((Z.Timeslot_Rate_Per_Day()) * 100).ToString("0");
            ViewBag.Week_Timeslot = ((Z.Timeslot_Rate_Per_Week()) * 100).ToString("0");
            ViewBag.Month_Timeslot = ((Z.Timeslot_Rate_Per_Month()) * 100).ToString("0");

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
    }
}