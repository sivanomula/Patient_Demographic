using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace HarAPI.ViewModels
{
    [XmlRoot("PhoneNumber")]
    public class PhoneNumber
    {
        [Required]
        [XmlElement("Type")]
        public PhoneType PhoneType { get; set; }
        [Required]
        [XmlElement("Number")]
        public string Number { get; set; }
        public string Type {
            get { return PhoneType.ToString(); }
        }
    }
}