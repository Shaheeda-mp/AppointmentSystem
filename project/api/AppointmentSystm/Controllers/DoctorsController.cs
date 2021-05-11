using AppointmentSystm.Data;
using AppointmentSystm.Data.Entities;
using AppointmentSystm.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSystm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly AppointmentDBContext context;
        public DoctorsController(AppointmentDBContext context)
        {
            this.context = context;
        }
        // GET: api/<AppointmentsController>
        [HttpGet]
        public IEnumerable<DoctorItem> Get()
        {
            var entities = this.context.Doctors.ToList();
            var dtos = entities.Select(X =>
            {
                return new DoctorItem
                {
                    Id = X.Id,
                    Name = X.Name
                };
            }).ToList();
            //return new ItemList<List<DoctorItem>> { Data=dtos};
            return dtos;
        }
        [HttpGet("Prepare")]
        public void PrepareItems()
        {
            var doctors = new List<Doctor>
            {
                new Doctor { Name="Till Lindemann"},
                new Doctor { Name="Richard Z. Kruspe"},
                new Doctor { Name="Paul H. Landers"}
            };
            context.Doctors.AddRange(doctors);
            context.SaveChanges();
        }
    }
}
