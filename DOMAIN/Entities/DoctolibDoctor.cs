using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
   public class DoctolibDoctor: Doctolib
    {
        public string nbreRPPS { get; set; }
        public string statuts { get; set; }
        public string nbreInscriptionOrdre { get; set; }
        public string nbreRCS { get; set; }
        public string formeJuridique { get; set; }
        public string adresseSocialSiege { get; set; }
        public string socialReason { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string skills { get; set; }
        public string presentationProfession { get; set; }
        public string horaires { get; set; }
      

    }
}
