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
    public class Coords
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid HotelInfoId { get; set; }

        [XmlElement("lat")]
        public double Lat { get; set; }

        [XmlElement("lng")]
        public double Lng { get; set; }
    }
}
