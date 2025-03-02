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
    public class HotelInfo
    {
        [Key]
        public Guid HotelInfoId { get; set; } = Guid.NewGuid();
        public Guid OfferId { get; set; }

        [MaxLength(30)]
        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("stars")]
        public int Stars { get; set; }

        [XmlElement("rating")]
        public double? Rating { get; set; } // Nullable, lebo nie vždy je prítomné

        [XmlElement("ratingcount")]
        public int? RatingCount { get; set; } // Nullable, lebo nie vždy je prítomné

        [XmlElement("coords")]
        public Coords Coords { get; set; }

        [MaxLength(150)]
        public string Url { get; set; }
    }
}
