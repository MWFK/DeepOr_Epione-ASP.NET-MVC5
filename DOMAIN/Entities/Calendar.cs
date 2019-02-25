using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Entities
{
    //[Table("Calendar")]
    public class Calendar
    {

        [Key]//in order for the key to work, the var needs to be public
        public int ID { get; set; }

        public int IdDoctor { get; set; }

        public DateTime dateCal { get; set; }

        public bool dispo { get; set; }
      
    }
}
