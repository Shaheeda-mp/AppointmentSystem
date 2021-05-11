using AppointmentSystm.Data;
using AppointmentSystm.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppointmentSystm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppointmentDBContext context;
        public AppointmentsController(AppointmentDBContext context)
        {
            this.context = context;
        }
        // GET: api/<AppointmentsController>
        [HttpGet]
        public IEnumerable<AppointmentDetails> Get()
        {
            var entities = this.context.Appointments.Include(X => X.Doctor).ToList();
            var dtos=entities.Select(oentity =>
            {
                return new AppointmentDetails
                {
                    Id = oentity.Id,
                    Name = oentity.Name,
                    Date = oentity.Date,
                    Email = oentity.Email,
                    Message = oentity.Message,
                    Doctor = oentity.Doctor.Name
                };
            }).ToList();
            return dtos;
        }

        // GET api/<AppointmentsController>/5
        [HttpGet("{id}")]
        public AppointmentDetails Get(int id)
        {
            var oentity = context.Appointments.Include(X=>X.Doctor).
                FirstOrDefault(Y=>Y.Id==id);
            var dto = new AppointmentDetails
            {
                Id=oentity.Id,
                Name = oentity.Name,
                Date=oentity.Date,
                Email=oentity.Email,
                Message=oentity.Message,
                Doctor=oentity.Doctor.Name
            };
            return dto;
        }

        // POST api/<AppointmentsController>
        [HttpPost]
        public ActionResult Post([FromBody] AppointmentCreate dto)
        {
            // If All Required Fields present
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Date should be future
            if (dto.Date < DateTime.Today)
            {
                ModelState.AddModelError("Date", "Min Date is Today");
                return BadRequest(ModelState);
            }
            // Time Range
            if(dto.Date.Hour<9 && dto.Date.Hour > 18)
            {
                ModelState.AddModelError("Time", "Time should be between 9AM and 6PM");
                return BadRequest(ModelState);
            }
            // One appointment per day per doctor
            if(context.Appointments.Any(X=>X.DoctorId==dto.DoctorId&&X.Date.Date==dto.Date.Date))
            {
                ModelState.AddModelError("DoctorId", "Only One Appointment Per Doctor Per Day");
                return BadRequest(ModelState);
            }
			// Limit Appointment Per Week
            var week = System.Globalization.ISOWeek.GetWeekOfYear(dto.Date);
            if (context.Appointments.Where(X=> System.Globalization.ISOWeek.GetWeekOfYear(X.Date)==week).Count()>5)
            {
                ModelState.AddModelError("Date", "Maximum Appointments Should be 5 per week");
                return BadRequest(ModelState);
            }
            var entity = new Data.Entities.Appointment
            {
                Name = dto.Name,
                Date=dto.Date,
                Email=dto.Email,
                Message=dto.Message,
                DoctorId=dto.DoctorId
            };
            context.Appointments.Add(entity);
            context.SaveChanges();
            return Ok();
        }

        // PUT api/<AppointmentsController>
        [HttpPut]
        public void Put([FromBody] AppointmentEdit dto)
        {
            var oentity = context.Appointments.Find(dto.Id);
            oentity.Name = dto.Name;
            oentity.Message = dto.Message;
            oentity.Email = dto.Email;
            //oentity.DoctorId = dto.DoctorId;

            context.SaveChanges();
        }

        // DELETE api/<AppointmentsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var oentity = context.Appointments.Find(id);
            context.Remove(oentity);
        }
    }
}
