using HarAPI.Models;
using HarAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace HarAPI.Controllers
{
    [Route("api/patient")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PatientsController : Controller
    {
        private readonly IDataRepository<Patient> _dataRepository;

        public PatientsController(IDataRepository<Patient> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        // GET: api/Employee
        [HttpGet]
        public IActionResult Get()
        {
            //pagination need to be added
            IEnumerable<Patient> patients = _dataRepository.GetAll();
            return Ok(patients.Select(p => p.ToPatientModel()));
        }

        // GET: api/Employee/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(long id)
        {
            Patient patient = _dataRepository.Get(id);

            if (patient == null)
            {
                return NotFound("The Patient record couldn't be found.");
            }
            var patientModel = patient.ToPatientModel();

            return Ok(patientModel);
        }

        // POST: api/Employee
        [HttpPost]        
        public IActionResult Post([FromBody] PatientModel patientModel)
        {
            if (patientModel == null)
            {
                return BadRequest("Patient is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            };

            var patient = new Patient { PatientXML = patientModel.ToXML() };
            _dataRepository.Add(patient);
            return CreatedAtRoute(
                  "Get",
                  new
                  {
                      Id = patient.Id
                  },
                  patient.ToPatientModel());
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] PatientModel patientModel)
        {
            if (patientModel == null)
            {
                return BadRequest("Employee is null.");
            }
            var patient = new Patient();
            Patient patientToUpdate = _dataRepository.Get(id);
            if (patientToUpdate == null)
            {
                return NotFound("The Patient record couldn't be found.");
            }

            _dataRepository.Update(patientToUpdate, patient);
            return NoContent();
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            Patient patient = _dataRepository.Get(id);
            if (patient == null)
            {
                return NotFound("The Patient record couldn't be found.");
            }

            _dataRepository.Delete(patient);
            return NoContent();
        }
    }
}
