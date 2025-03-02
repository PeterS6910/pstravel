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
    public class Destination
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OfferId { get; set; }

        [MaxLength(80)]
        [XmlElement("country")]
        public string Country { get; set; }

        [MaxLength(120)]
        [XmlElement("locality")]
        public string Locality { get; set; }
       
    }
}
