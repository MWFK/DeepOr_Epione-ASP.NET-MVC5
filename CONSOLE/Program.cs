using SERVICE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CONSOLE
{
    public class Program
    {
        static void Main(string[] args)
        {
            ICancellation_Rate_Service X = new Cancellation_Rate_Service();

            //Console.WriteLine(X.Cancellation_Rate_Per_Week());
            float x=19.7575F;
            Console.WriteLine(x.ToString("0.00"));

            Console.Read();
        }

    }
}