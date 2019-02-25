using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
   public class Doctolib
    {
    
        public int doctolibId { get; set; }
        public string name { get; set; }
        public string img { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string path { get; set; }
        public string speciality { get; set; }
    }
}
