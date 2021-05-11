using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentSystm.Data.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
