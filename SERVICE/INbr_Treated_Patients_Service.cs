using DOMAIN.Entities;
using SERVICEPATTERN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public interface INbr_Treated_Patients_Service:IService<Appointment>
    {

        int Count_Nbr_Treated_Patients_Per_Day();
        int Count_Nbr_Treated_Patients_Per_Month();
        int Count_Nbr_Treated_Patients_Per_Week();


        /* 
               ***     jai  essayer  derendre tt ces methode des web services      ***

           string Afficher();

           int INTAfficher();

           int GetallAfficher();

           IEnumerable<Appointment> ListAfficher();
       */

    }
}
