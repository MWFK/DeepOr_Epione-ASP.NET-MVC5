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
    public class Cancellation_Rate_Service : Service<Appointment>, ICancellation_Rate_Service
    {
        private static IDatabaseFactory databaseFactory = new DatabaseFactory();
        private static IUnitOfWork utwk = new UnitOfWork(databaseFactory);

        public Cancellation_Rate_Service() : base(utwk)
        {
        }

        
        public float Cancellation_Rate_Per_Day()
        {
            var A = GetAll();

            var Cancel = (from i in A //i changed a => a.date == DateTime.Today to a => a.date.Date == DateTime.Today, and no need to verify the month and the year (I inserted data that proves that)
                          where i.IdDoctor == 1 && i.date.Date == DateTime.Today && i.status == false
                          select i.date
                        ).Count();

            float y = Cancel;
            //i need to chnage a => a.date == DateTime.Today to a => a.date.Date == DateTime.Today
            //because of the Time part, but link does not support a => a.date.Date == DateTime.Today
            //float x = GetMany(a => a.date == DateTime.Today, b => b.IdDoctor == 1).Count();
            var All = (from i in A 
                          where i.IdDoctor == 1 && i.date.Date == DateTime.Today
                          select i.date
                        ).Count();

            float x = All;

            //int result = (int)Math.Round((y / x)*100);

            //GetMany can take one or two arguments, no more
            //return var/var gives 0
            //i changed the return value to int cuz there is to much decimal numbers
            return y/x;
        }

        public float Cancellation_Rate_Per_Month()
        {
            var All = GetAll();

            var Cancel = (from i in All
                          where i.IdDoctor == 1 && i.date.Month == DateTime.Today.Month && i.date.Year == DateTime.Today.Year && i.status == false
                          select i.date
                        ).Count();

            var Total = (from i in All
                          where i.IdDoctor == 1 && i.date.Month == DateTime.Today.Month && i.date.Year == DateTime.Today.Year
                          select i.date
                        ).Count();

            float y = Cancel;
            float x = Total;

            //int result = (int)Math.Round((y / x)*100);

            //GetMany can take one or two arguments, no more
            //return var/var gives 0
            //i changed the return value to int cuz there is to much decimal numbers
            return y/x;

        }

        public float Cancellation_Rate_Per_Week()
        {
            var A = GetAll();

            int Total = 0, Cancel = 0, TCancel = 0;

            for (int Day = 0; Day < 7; Day++)
            {
                foreach (var item in A)
                {

                    if (item.date.Date == DateTime.Today.AddDays(-Day) && item.IdDoctor == 1)
                    {
                        Total++;
                    }

                    if (item.date.Date == DateTime.Today.AddDays(-Day) && item.IdDoctor == 1 && item.status == false)
                    {
                        Cancel++;
                    }
                }

                TCancel = TCancel + Cancel;
                Cancel = 0;
            }

            //dans le C# return int/int ne donne pas un float meme si la valeur de retour de la methode est float
            float x = Total;
            float y = TCancel;
            
            //int result = (int)Math.Round((y / x)*100);
            //Console.WriteLine(result);

            return y/x;
        }



    }
}
