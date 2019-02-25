using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpioneMVC.Models
{
    public class DoctolibViewModel
    {
        public string name { get; set; }
        public string img { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string path { get; set; }
        public string speciality { get; set; }
        public string nbreRPPS { get; set; }
        public string statuts { get; set; }
        public string nbreInscriptionOrdre { get; set; }
        public string nbreRCS { get; set; }
        public Boolean memberAGA { get; set; }
        public string formeJuridique { get; set; }
        public string adresseSocialSiege { get; set; }
        public string socialReason { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public List<string> skills { get; set; }
        public string presentationProfession { get; set; }
        public string Horaires { get; set; }
        public List<string> openningDays { get; set; }
        public List<string> openningtimes { get; set; }

    }
}