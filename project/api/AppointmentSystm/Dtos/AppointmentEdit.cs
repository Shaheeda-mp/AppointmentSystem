﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSystm.Dtos
{
    public class AppointmentEdit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public int DoctorId { get; set; }
    }
}
