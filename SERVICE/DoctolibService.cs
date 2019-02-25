using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using System.Xml.Linq;
using HtmlAgilityPack;
using System.Net;
using SERVICEPATTERN;
using DATA.Infrastructure;
using System.Web.Script.Serialization;

namespace SERVICE
{
    public class DoctolibService : Service<Doctor>,IDoctolibService
    {
        private static IDatabaseFactory dbFactory = new DatabaseFactory();
        private static IUnitOfWork uow = new UnitOfWork(dbFactory);

        public DoctolibService() : base(uow)
        {
        }

        //**************************MAP***********************////
        public string getDistance(string adresse1,string adresse2)
        {
            List<string> LatLng1 = new List<string>();
            List<string> LatLng2 = new List<string>();
            LatLng1 = DoctolibService.convertadd(adresse1);
            LatLng2 = DoctolibService.convertadd(adresse2);
            string dis1 = LatLng1[0]+"+"+LatLng1[1];
            string dis2 = LatLng2[0]+"+"+LatLng2[1];

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("http://boulter.com/gps/distance/?from="+dis1+"&to="+dis2+"&units=k");
            return (DoctolibService.betweenStrings(doc.ParsedText, "</FONT></TD><TD>", " kilometers").Trim());
        }
       
            public static List<String> convertadd(string Adress)
            {
                List<String> str = new List<string>();
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load("https://www.google.com/maps/search/" + Adress);
                var Path = doc.DocumentNode.SelectNodes("//meta").ToList();

                for (var i = 0; i < Path.LongCount(); i++)
                {
                    HtmlAttribute pathatt = Path[i].Attributes["property"];
                    if (pathatt != null)
                    {
                        if (pathatt.Value == "og:image")
                        {
                            HtmlAttribute secondpath = Path[i].Attributes["content"];

                            String loong = secondpath.Value.Substring(50, 8).ToString();
                            String lat = secondpath.Value.Substring(63, 8).ToString();

                            str.Add(loong);
                            str.Add(lat);

                            return str;
                        }
                    }

                }

                return null;
            }
            //**************************MAP***********************////
            public DoctolibDoctor getDoctorByPath(string path)
        {
            DoctolibDoctor doctor = new DoctolibDoctor();
            List<string> skillsList = new List<string>();
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load("https://www.doctolib.fr/"+path);
            var Name = doc.DocumentNode.SelectSingleNode("//h1[@class='dl-profile-header-name']");

            var Image= doc.DocumentNode.SelectSingleNode("//div[@class='dl-profile-header-photo']/img[@src]");
           HtmlAttribute srcImage = Image.Attributes["src"];

            var Adresse = doc.DocumentNode.SelectSingleNode("//div[@class='dl-profile-practice-name']");
      

            var Horaire = doc.DocumentNode.SelectSingleNode("//div[@class='js-opening-hours']");
            if (Horaire != null)
            {
                HtmlAttribute horaire = Horaire.Attributes["data-props"];
                doctor.horaires = horaire.Value.Replace("&quot;", "'");
            }
            
            var position = doc.DocumentNode.SelectSingleNode("//img[@class='dl-profile-doctor-place-map-img']");
            HtmlAttribute mapAttribute = position.Attributes["data-map-modal"];
            string LatMap = mapAttribute.Value.Substring(mapAttribute.Value.IndexOf("lat&quot;:") +10, 8);
            string LongMap = mapAttribute.Value.Substring(mapAttribute.Value.IndexOf("lng&quot;:") + 10, 8);
           // string city = path.IndexOf("/", path.IndexOf("/", 2)+ 1).ToString();

                var speciality = doc.DocumentNode.SelectSingleNode("//h2[@class='dl-profile-header-speciality']");

               var Formations = doc.DocumentNode.SelectNodes("//div[@class='dl-profile-entry-label']");
            if (Formations != null)
            {
               var  Formation =Formations.ToList();
                foreach (var formation in Formation)
                {
                    skillsList.Add(formation.InnerText.Replace("&#39;", "'"));
                }
            }
            
            var Skills = doc.DocumentNode.SelectNodes("//div[@class='dl-profile-skill-chip']");
            var motifs= doc.DocumentNode.SelectNodes("//div[@class='dl-profile-fee-name']");
            var tarifs= doc.DocumentNode.SelectNodes("//div[@class='dl-profile-fee-tag']");
            if (Skills != null)
            {
                var Skill = Skills.ToList();
                doctor.skills = "";
                foreach (var skill in Skill)
                {
                    skillsList.Add(skill.InnerText.Replace("&#39;", "'"));
                    doctor.skills = doctor.skills + "|" + skill.InnerText.Replace("&#39;", "'");
                }
                doctor.skills = doctor.skills.Substring(0);
            }
            if (motifs != null)
            {
                var motif = motifs.ToList();
                doctor.formeJuridique = "";
                foreach (var m in motif)
                {
                   // skillsList.Add(skill.InnerText.Replace("&#39;", "'"));
                    doctor.formeJuridique = doctor.formeJuridique + "|" + m.InnerText.Replace("&#39;", "'");
                }
                var tarif = tarifs.ToList();
                doctor.adresseSocialSiege = "";
                foreach (var t in tarif)
                {
                    // skillsList.Add(skill.InnerText.Replace("&#39;", "'"));
                    doctor.adresseSocialSiege = doctor.adresseSocialSiege + "|" + t.InnerText.Replace("&#39;", "'");
                }
            }
            var presentation = doc.DocumentNode.SelectSingleNode("//div[@class='dl-profile-text js-bio dl-profile-bio']");
            var adressDoctor = doc.DocumentNode.SelectSingleNode("/html/body/div[4]/div[2]/div[1]/div[4]/div/div[2]/div[1]/div");
            doctor.name = Name.InnerText.Replace("&#39;", "'");
            doctor.img = srcImage.Value;
            if (adressDoctor != null)
            {
                doctor.city = adressDoctor.InnerText;
            }
            
            doctor.address = Adresse.InnerText.Replace("&#39;", "'").Replace("&quot;", "'");
            doctor.lat = LatMap;
            doctor.lng = LongMap;
            doctor.speciality = speciality.InnerText.Replace("&#39;", "'").Replace("&quot;", "'");
            

           // doctor.skills = skillsList;
            doctor.presentationProfession = presentation.InnerText.Replace("&#39;", "'").Replace("&quot;", "'");

            return doctor;
        }

        public List<Doctolib> getListDoctorsBySpecialityAndLocation(string speciality, string location, string page)
        {
            List<Doctolib> lst=new List<Doctolib>();
            Doctolib doctor;
            
            if (location==null)
            {
                location = "france";
            }
            if (page==null)
            {
                page = "0";
            }
            if (speciality == null)
            {
                speciality = "medecin-generaliste";
            }



            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc;
   try
            {
                 doc = web.Load("https://www.doctolib.fr/" + speciality + "/" + location + "?page=" + page);

            }
            catch
            {
                try
                {
                    doc = web.Load("https://www.doctolib.fr/" + speciality + "/france?page=" + page);

                }
                catch
                {
                    doc = web.Load("https://www.doctolib.fr/medecin-generaliste/france?page=" + page);
                }

            }

            var Names = doc.DocumentNode.SelectNodes("//a[@class='dl-search-result-name js-search-result-path']/div").ToList();
           
            var Specialties = doc.DocumentNode.SelectNodes("//div[@class='dl-search-result-subtitle']").ToList();
            var Address= doc.DocumentNode.SelectNodes("//div[@class='dl-text dl-text-body']").ToList();

            HtmlNodeCollection Images = doc.DocumentNode.SelectNodes("//img[@src]");
            HtmlNodeCollection Paths = doc.DocumentNode.SelectNodes("//a[@class='dl-search-result-name js-search-result-path']");

            for (var i=0;i< Names.LongCount();i++)
            {
                HtmlAttribute pathDoctor= Paths[i].Attributes["href"];
                HtmlAttribute src = Images[i].Attributes["src"];
                doctor = new Doctolib() { name = Names[i].InnerText.Replace("&#39;", "'"), img = src.Value, speciality = Specialties[i].InnerText.Replace("&#39;", "'"), address = Address[i].InnerText.Replace("&#39;", "'"), path= pathDoctor.Value };
                lst.Add(doctor);
            }
            return lst;
        }
        public List<DoctolibDoctor> filterListDoctorsBySpecialityAndLocation(string speciality, string location, string availabilities)
        {
            List<string> listDisponibility = new List<string>();
            List<string> listLinkDoctors = new List<string>();

            List<DoctolibDoctor> lst = new List<DoctolibDoctor>();

            if (location == null)
            {
                location = "france";
            }
            
            if (speciality == null)
            {
                speciality = "medecin-generaliste";
            }



            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc;
            try
            {
                doc = web.Load("https://www.doctolib.fr/" + speciality + "/" + location + "?availabilities=" + availabilities);

            }
            catch
            {
                try
                {
                    doc = web.Load("https://www.doctolib.fr/" + speciality + "/france?availabilities=" + availabilities );

                }
                catch
                {
                    try
                    {
                        doc = web.Load("https://www.doctolib.fr/medecin-generaliste/france?availabilities=" + availabilities);

                    }
                    catch
                    {
                        doc = web.Load("https://www.doctolib.fr/medecin-generaliste/france");

                    }
                }

            }
            listDisponibility.Add(DoctolibService.betweenStrings(doc.ParsedText.ToString(), "searchResultIds&quot;:[", "],&quot;availabilitiesLimit&"));
            listDisponibility.Add(DoctolibService.betweenStrings(doc.ParsedText.ToString(), ",&quot;specialityId&quot;:", ",&quot;title&quot;:"));
            listDisponibility.Add(availabilities);

            String[] searchresult = listDisponibility[0].Split(',');
            List<DoctolibDoctor> doctors = new List<DoctolibDoctor>();
            for (int i = 0; i < searchresult.Length; i++)
            {
                var json=new WebClient().DownloadString("https://www.doctolib.fr/search_results/" + searchresult[i] + ".json?search_result_format=json&speciality_id=" + listDisponibility[1] + "&limit=" + listDisponibility[2] + "&availability_filter=true");
                string total = DoctolibService.betweenStrings(json, "\"total\":", ",\"");
                
                if (total != "0")
                {
                    

                    string link = DoctolibService.betweenStrings(json, ",\"link\":\"", "\",\"");
                    string address = DoctolibService.betweenStrings(json, ",\"address\":\"", "\",\"")+ DoctolibService.betweenStrings(json, ",\"city\":\"", "\",\"");
                    string img = "//res.cloudinary.com/doctolib/image/upload/w_160,h_160,c_fill,g_face/" + DoctolibService.betweenStrings(json, ",\"cloudinary_public_id\":\"", "\",\"") + ".jpg";
                    string lat = DoctolibService.betweenStrings(json, "\"lat\":", ",\"");
                    string lng = DoctolibService.betweenStrings(json, ",\"lng\":", "},\"");
                    string name = DoctolibService.betweenStrings(json, ",\"name_with_title\":\"", "\",\"");

                  
                    DoctolibDoctor doctor = new DoctolibDoctor() {name=name,img=img,address=address,lat=lat,lng=lng,path=link };
                    lst.Add(doctor); 
                }
                
            }


            return lst;
           
        }

        public DoctolibOther getOtherByPath(string path)
        {
            throw new NotImplementedException();
        }

        public string getPath(string speciality, string fullname, int rpps)
        {
            string events = new WebClient().DownloadString("https://www.doctolib.fr/api/searchbar/autocomplete.json?search=" + fullname + " " + speciality).Replace("Ã©", "é");

            
        
            return betweenStrings(events, "link\":\"", "\"}");
        }

        public bool RegisterWithDoctolib(string speciality, string fullname, int rpps)
        {
            string events = new WebClient().DownloadString("https://www.doctolib.fr/api/searchbar/autocomplete.json?search=" +fullname+" "+speciality).Replace("Ã©", "é");
            return events.Contains(rpps.ToString()); 
        }
        public static String betweenStrings(String text, String start, String end)
        {
            int p1 = text.IndexOf(start) + start.Length;
            int p2 = text.IndexOf(end, p1);

            if (end == "") return (text.Substring(p1));
            else return text.Substring(p1, p2 - p1);
        }

        public IEnumerable<Doctor> RequestManagement()
        {
          
            return GetAll().Where(t => (t.status == 0) && (t.Role.Equals("Doctor"))).ToList(); 
        }

        public IEnumerable<Doctor> listAcceptedRegister()
        {
            return GetAll().Where(t => (t.status == 1) && (t.Role.Equals("Doctor"))).ToList();
        }

        public IEnumerable<Doctor> listRefusedRegister()
        {
            return GetAll().Where(t => (t.status == 2) && (t.Role.Equals("Doctor"))).ToList();
        }

        public void UpdateStatus(Doctor doc)
        {
            uow.getRepository<Doctor>().Update(doc);
            uow.Commit();
        }

        public int DoctolibRequest()
        {
            return GetAll().Where(t => (t.status == 0) && (t.Role.Equals("Doctor"))).Count();
        }

        public int DoctolibAccepted()
        {
            return GetAll().Where(t => (t.status == 1) && (t.Role.Equals("Doctor"))).Count();
        }

        public int DoctolibRefused()
        {
            return GetAll().Where(t => (t.status == 2)&&(t.Role.Equals("Doctor"))).Count();
        }
    }
}
