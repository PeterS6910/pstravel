using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ImportXml.AfiTravelModel
{
    [XmlRoot("Person")]
    public class Offers
    {
        [Key]
        public Guid Id { get; set; }

        [XmlElement("Timestamp")]
        public DateTime Timestamp { get; set; }

        [XmlElement("Count")]
        public int Count { get; set; }

        [XmlElement("Offer")]
        public List<Offer> Offer { get; set; } = new List<Offer>();
    }
}
