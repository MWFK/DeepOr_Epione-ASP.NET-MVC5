using Domain.Entities;
using SERVICE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class DotolibAPIController : ApiController
    {
        IDoctolibService service = new DoctolibService();

        // GET: api/DotolibAPI
        [Route("api/doctolib")]
        //http://localhost:54656/api/doctolib?speciality=&location=&page
        public IEnumerable<DoctolibDoctor> Get(string speciality, string location, string page)
        {
            List<DoctolibDoctor> doctolibs = new List<DoctolibDoctor>();
            foreach (var item in service.getListDoctorsBySpecialityAndLocation(speciality, location, page))
            {
                DoctolibDoctor doctolib = new DoctolibDoctor();
                doctolib = service.getDoctorByPath(item.path);
                doctolib.name = item.name;
                doctolib.img = item.img;
                doctolib.path = item.path;
                doctolib.address = item.address;
                doctolib.speciality = item.speciality;
                doctolibs.Add(doctolib);

            }
            return  doctolibs ;
        }
        // GET: api/DotolibAPI
        [Route("api/doctolibfilter")]

        public List<DoctolibDoctor> GetDoctor(string speciality, string location, string availabilities)
        {

            return service.filterListDoctorsBySpecialityAndLocation(speciality, location, availabilities);
        }
        // GET: api/DotolibAPI/5
        //public IEnumerable<Doctolib> Get(string speciality)
        //{
        //    List<Doctolib> doctolibs = new List<Doctolib>();
        //    foreach (var item in service.getListDoctorsBySpecialityAndLocation(speciality, "", ""))
        //    {
        //        Doctolib doctolib = new Doctolib();
        //        doctolib.name = item.name;
        //        doctolib.img = item.img;
        //        doctolib.path = item.path;
        //        doctolib.address = item.address;
        //        doctolibs.Add(doctolib);

        //    }
        //    return doctolibs;
        //}

        // GET: api/DotolibAPI
        [Route("api/doctolibdoctor")]

        public DoctolibDoctor GetDoctor(string link)
        {
           DoctolibDoctor doctolibs = new DoctolibDoctor();
            doctolibs = service.getDoctorByPath(link);
            return doctolibs;
         
        }

        // POST: api/DotolibAPI
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/DotolibAPI/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DotolibAPI/5
        public void Delete(int id)
        {
        }
    }
}
