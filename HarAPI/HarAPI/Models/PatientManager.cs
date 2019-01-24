using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HarAPI.ViewModels;

namespace HarAPI.Models
{
    public class PatientManager : IPatientManager
    {
        readonly HarAPIContext harAPIContext;

        public PatientManager(HarAPIContext context)
        {
            harAPIContext = context;
        }

        public IEnumerable<Patient> GetAll()
        {
            return harAPIContext.Patients.ToList();
        }

        public Patient Get(long id)
        {
            return harAPIContext.Patients
                  .FirstOrDefault(e => e.Id == id);
        }

        public int Add(Patient entity)
        {
            harAPIContext.Patients.Add(entity);
            harAPIContext.SaveChanges();
            return entity.Id;
        }

        public void Update(Patient patient, Patient entity)
        {
            patient.PatientXML = entity.PatientXML;
            harAPIContext.SaveChanges();
        }

        public void Delete(Patient patient)
        {
            harAPIContext.Patients.Remove(patient);
            harAPIContext.SaveChanges();
        }
    }
}
