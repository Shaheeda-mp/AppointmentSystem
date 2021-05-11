using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSystm.Dtos
{
    public class AppointmentCreate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public int DoctorId { get; set; }
    }
}
