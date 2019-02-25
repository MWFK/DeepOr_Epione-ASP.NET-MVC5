using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpioneMVC.Models
{
    public class AppointmentViewModelMK
    {
        public int IdDoctor { get; set; }
        public int IdPatient { get; set; }
        public DateTime date { get; set; }
        public bool status { get; set; }

    }
}