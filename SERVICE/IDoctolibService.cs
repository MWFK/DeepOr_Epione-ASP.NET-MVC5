using Domain.Entities;
using SERVICEPATTERN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
   public interface IDoctolibService:IService<Doctor>
    {
        List<Doctolib> getListDoctorsBySpecialityAndLocation(string speciality, string location, string page) ;
         DoctolibDoctor getDoctorByPath(string path);
         DoctolibOther getOtherByPath(string path) ;
        bool RegisterWithDoctolib(string speciality,string fullname,int rpps);
        string getPath(string speciality, string fullname, int rpps);
        IEnumerable<Doctor> RequestManagement();
        IEnumerable<Doctor> listAcceptedRegister();
        IEnumerable<Doctor> listRefusedRegister();
        void UpdateStatus(Doctor doc);
        int DoctolibRequest();
        int DoctolibAccepted();
        int DoctolibRefused();
        List<DoctolibDoctor> filterListDoctorsBySpecialityAndLocation(string speciality, string location, string availabilities);


    }
}
