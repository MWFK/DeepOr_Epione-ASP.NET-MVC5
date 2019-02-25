using DOMAIN.Entities;
using SERVICEPATTERN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATA.Infrastructure;

namespace SERVICE
{
    public class Used_Timeslot_Rate_Service : Service<Calendar>, IUsed_Timeslot_Rate_Service
    {
        private static IDatabaseFactory databaseFactory = new DatabaseFactory();
        private static IUnitOfWork utwk = new UnitOfWork(databaseFactory);

        
        //public Used_Timeslot_Rate_Service(IUnitOfWork utwk) : base(utwk) 
        //we add this constructor if our service use an argument
        //but in this case we delete IUnitOfWork utwk cuz we dont need it
        public Used_Timeslot_Rate_Service() : base(utwk)
        {
        }




//**********************************************************************************************************
//**********************************************************************************************************
        public float Timeslot_Rate_Per_Day()
        {

//********************************************Number of used Timeslots
            var C = GetAll();
            var A = utwk.getRepository<Appointment>().GetAll();

            //in the table calendar we compare all dateCal(only the date part) to the current day
            var QueryC = (
                            from i in C
                            where i.IdDoctor==1 && i.dispo==true && i.dateCal.Date==DateTime.Today
                            select i/*.dateCal */
                        );

            //in the table calendar we compare all date(only the date part) to the current day
            var QueryA = (
                           from i in A
                           where i.IdDoctor == 1 && i.status == true && i.date.Date == DateTime.Today
                           select i/*.date*/
                       );


            int Used_Timeslot = 0;
            //a chaque fois on va fixer une date de calendar et va la comparer(date et time) avec tt les date de appointment
            foreach(var itemC in QueryC)
            {
                foreach(var itemA in QueryA)
                {
                   if(Equals(itemC.dateCal,itemA.date))
                    { Used_Timeslot++; }
                }
            }

            float Used = Used_Timeslot;
//************************************Number of Open TimeSlots
            float All = QueryC.Count();
     
            return Used/All;

            
            
            //********************************************Number of used Timeslots (other ways)
            //second method 
            //var result = from x in entity1
            //             join y in entity2 
            //             on new { X1 = x.field1, X2 = x.field2 } equals new { X1 = y.field1, X2 = y.field2 }
            //              select {x,y};
            
            //third method
            //var result = from x in entity1
            //             from y in entity2
            //                              .Where(y => y.field1 == x.field1 && y.field2 == x.field2)
        }





//**********************************************************************************************************
//**********************************************************************************************************

        public float Timeslot_Rate_Per_Month()
        {

            //********************************************Number of used Timeslots
            var C = GetAll();
            var A = utwk.getRepository<Appointment>().GetAll();

            //in the table calendar we compare all dateCal(only the date part) to the current Month
            var QueryC = (
                            from i in C                                     //we only take the date from both of the datetime (in the same day we can have mltiple diffrent times)
                            where i.IdDoctor == 1 && i.dispo == true && i.dateCal.Month == DateTime.Today.Month && i.dateCal.Year == DateTime.Today.Year
                            select i/*.dateCal */
                        );

            //in the table calendar we compare all date(only the date part) to the current Month
            var QueryA = (
                           from i in A
                           where i.IdDoctor == 1 && i.status == true && i.date.Month == DateTime.Today.Month && i.date.Year == DateTime.Today.Year
                           select i/*.date*/
                       );


            int Used_Timeslot = 0;
            //a chaque fois on va fixer une date de calendar et va la comparer(date et time) avec tt les date de appointment
            foreach (var itemC in QueryC)
            {
                foreach (var itemA in QueryA)
                {
                    if (Equals(itemC.dateCal, itemA.date))
                    { Used_Timeslot++; }
                }
            }

            float Used = Used_Timeslot;


//************************************Number of Open TimeSlots
            float All = QueryC.Count();
            float Open = All - Used;

            return Used / All;
        }


//**********************************************************************************************************
//**********************************************************************************************************


        public float Timeslot_Rate_Per_Week()
        {
//********************************************Number of used Timeslots

            //**Extracting the dates of the current week from the calendar where the docotor is available**//
            var C = GetAll();

            List<DateTime> ListC = new List<DateTime>();

            for (int Day = 0; Day < 7; Day++)
            {
                foreach (var item in C)
                {

                    if (item.dateCal.Date == DateTime.Today.AddDays(-Day) && item.IdDoctor == 1 && item.dispo == true)
                    {
                        ListC.Add(item.dateCal);                        
                    }
                }
            }

            //for (int i = 0; i < ListC.Count(); i++)
            //{ Console.WriteLine(ListC[i]); }


            //**Extracting the dates of the current week from the appointment where the docotor is available**//
            var A = utwk.getRepository<Appointment>().GetAll();

            List<DateTime> ListA = new List<DateTime>();

            for (int Day = 0; Day < 7; Day++)
            {
                foreach (var item in A)
                {
                    if (item.date.Date == DateTime.Today.AddDays(-Day) && item.IdDoctor == 1 && item.status == true)
                    {
                        ListA.Add(item.date);
                    }
                }
            }

            //for (int i = 0; i < ListA.Count(); i++)
            //{ Console.WriteLine(ListA[i]); }

            //********Matching Calendar dates with Appointments dates
            int Used = 0;
            for(int i=0;i<ListC.Count();i++)
            {
                for(int j=0;j<ListA.Count();j++)
                {
                    if (Equals(ListC[i], ListA[j]))
                        Used++;
                }
            }

//***********************************Number of Open Time Slot
            //si je fais pas la conversion de ListC.Count(), return va donner 0
            //car ListC.Count() et de type var, mais elle chnage selon le contexte
            //le probleme c'est que Used est de type int et la methode retourne float
            //donc on va avoir une confusion de type return(float) Used(int)/ListC.Count()(var)
            float All = ListC.Count();

            return Used/All;
        }
    }
}
