using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using AppointmentSystm.Data.Entities;

namespace AppointmentSystm.Data
{
    public class AppointmentDBContext : DbContext
    {
        public AppointmentDBContext(DbContextOptions<AppointmentDBContext> options) : base(options)
        {
        }


        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
    }
}
