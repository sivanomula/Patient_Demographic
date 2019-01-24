using HarAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace HarAPI.ViewModels
{
    public static class ModelExtensions
    {
        public static PatientModel ToPatientModel(this Patient patient)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PatientModel));
            using (TextReader reader = new StringReader(patient.PatientXML))
            {
                var result = (PatientModel)serializer.Deserialize(reader);
                result.Id = patient.Id;
                return result;
            }
        }
        public static string ToXML(this PatientModel patientModel)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(PatientModel));
            //var subReq = new MyObject();
            var xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, patientModel);
                    xml = sww.ToString(); // Your XML
                }
            }
            return xml;
        }
    }
}
