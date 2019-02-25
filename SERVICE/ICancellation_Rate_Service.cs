using DOMAIN.Entities;
using SERVICEPATTERN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public interface ICancellation_Rate_Service : IService<Appointment>
    {
        float Cancellation_Rate_Per_Day();
        float Cancellation_Rate_Per_Month();
        float Cancellation_Rate_Per_Week();
    }
}
