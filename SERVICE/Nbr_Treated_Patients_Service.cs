using DOMAIN.Entities;
using SERVICEPATTERN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATA.Infrastructure;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace SERVICE
{
    public class Nbr_Treated_Patients_Service : Service<Appointment>, INbr_Treated_Patients_Service
    {
        private static IDatabaseFactory databaseFactory = new DatabaseFactory();
        private static IUnitOfWork utwk = new UnitOfWork(databaseFactory);

        public Nbr_Treated_Patients_Service() : base(utwk)
        {
        }

        //GetMany returns IEnumerable < Entity >its like GetAll
        //but it can order the element, and we can have inside LINQ expressions

        public int Count_Nbr_Treated_Patients_Per_Day()
        {
            var A = GetAll();
            // ou var A = GetMany();
            var query = (from i in A
                         where i.IdDoctor == 1 && i.date.Date == DateTime.Today && i.status == true
                         select i.date
                        ).Count();

            return query;     
        }

//**************************************************************************************************
//**************************************************************************************************

        public int Count_Nbr_Treated_Patients_Per_Month()
        {
            var A = GetAll();
            var query = (from i in A
                         where i.IdDoctor == 1 && i.date.Month == DateTime.Now.Month && i.date.Year == DateTime.Now.Year && i.status == true
                         select i.date
                        ).Count();
            return query;
        }

        public int Count_Nbr_Treated_Patients_Per_Week()
        {
            var A = GetAll();

            int Nbr_Day = 0, Nbr_Week = 0;

            for (int Day = 0; Day < 7; Day++)
            {
                foreach (var item in A)
                    {
                    
                        if (item.date.Date == DateTime.Today.AddDays(-Day) && item.IdDoctor == 1 && item.status == true)
                            {
                                Nbr_Day++;
                            }
                    }

                Nbr_Week = Nbr_Week + Nbr_Day;
                Nbr_Day = 0;
            }

            return Nbr_Week;
        }


        /* 
               ***     jai  essayer  derendre tt ces methode des web services      ***
           public string Afficher() { return "20"; }

           public int INTAfficher() { return 20; }

           public int GetallAfficher()
           {
               return GetAll().Count();
           }

           public IEnumerable<Appointment> ListAfficher()
           {
               List<Appointment> c = new List<Appointment>();
                c = GetAll().ToList();
               return c;
           }

       */

        //This solution violates the .net architecture, cause we need to be using ORM (LINQ)
        //one the ssues that will happen is the conection with the database
        //***************SQL DateTime ISO 8601****************//
        //[STAThread]
        //public int Count_Nbr_Treated_Patients()
        //{

        //    SqlConnection conn = new SqlConnection("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=epione_db;");

        //    SqlDataReader dr;

        //    int Nbr_Treated_Patients = 0;

        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.CommandTimeout = 60;
        //        cmd.Connection = conn;
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = "SELECT Count(*) FROM Appointments";

        //        conn.Open();
        //        if (conn.State == ConnectionState.Open)
        //        {
        //            object objCount = cmd.ExecuteScalar();
        //            int iCount = (int)objCount;

        //            Console.Write("Donner une date sous cette format MM/DD/YYYY\t");
        //            string DateUser = Console.ReadLine();


        //            Console.Write("Donner IdDoctor: ");
        //            string SID = Console.ReadLine();
        //            int ID = Convert.ToInt32(SID);
        //            cmd.Parameters.AddWithValue("@ID", ID);

        //            cmd.CommandText = "SELECT date,IdDoctor FROM Appointments";
        //            dr = cmd.ExecuteReader(CommandBehavior.SingleResult);

        //            for (int i = 0; i < iCount; i++)
        //            {
        //                dr.Read();
        //                string SDate = dr[0].ToString();
        //                int IID = Convert.ToInt32(dr[1]);
        //                DateTime DTDate = Convert.ToDateTime(SDate);
        //                string DTDATE_ISO = DTDate.Month + "/" + DTDate.Day + "/" + DTDate.Year;

        //                if (DTDATE_ISO == DateUser && IID == ID) { Nbr_Treated_Patients++; }
        //            }

        //        }
        //    }

        //    catch (Exception exp)
        //    {
        //        Console.Write(exp.Message);
        //    }

        //    finally
        //    {
        //        conn.Close();
        //    }


        //    return Nbr_Treated_Patients;
        //}

    }
}
