using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSystm.Dtos
{
    public class AppointmentItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
