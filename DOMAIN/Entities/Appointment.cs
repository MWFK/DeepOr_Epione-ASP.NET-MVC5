using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Entities
{
    public class Appointment
    {

        [Key]
        public int ID { get; set; }

        public int IdDoctor { get; set; }

        public DateTime date { get; set; }

        public bool status { get; set; }
    }
}
