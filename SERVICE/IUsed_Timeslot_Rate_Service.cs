using DOMAIN.Entities;
using SERVICEPATTERN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public interface IUsed_Timeslot_Rate_Service : IService<Calendar>
    {
        float Timeslot_Rate_Per_Day();
        float Timeslot_Rate_Per_Month();
        float Timeslot_Rate_Per_Week();
    }
}
