using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ImportXml.AfiTravelModel
{
    [XmlRoot("offers")]
    public class Offers
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [XmlIgnore]
        //[XmlElement("timestamp")]
        public DateTime Timestamp { get; set; }

        [XmlElement("count")]
        public int Count { get; set; }

        [XmlElement("offer")]
        public List<Offer> Offer { get; set; } = new List<Offer>();
    }
}
