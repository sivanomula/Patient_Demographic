using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HarAPI.ViewModels
{
    [XmlRoot("Patient")]
    public class PatientModel
    {
        [XmlIgnore]
        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Fornames should be minimum 3 characters")]
        [MaxLength(50, ErrorMessage = "Fornames should be maximum 50 characters")]
        [XmlElement("Forenames")]
        public string Forenames { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Fornames should be minimum 2 characters")]
        [MaxLength(50, ErrorMessage = "Fornames should be maximum 50 characters")]
        [XmlElement("Surname")]
        public string Surname { get; set; }

        [XmlElement("Gender")]
        public string Gender { get; set; }

        [Required]
        [XmlElement("DateofBirth")]
        public DateTime DateofBirth { get; set; }

        [Required]        
        [XmlElement("Telephonenumbers")]
        public List<PhoneNumber> Telephonenumbers { get; set; }

    }
}
