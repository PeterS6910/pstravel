using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ImportXml.AfiTravelModel
{
    public class Image
    {
        [Key]
        public Guid OfferId { get; set; }

        // Hodnota medzi tagmi <image> (URL obrázku)
        [MaxLength(250)]
        [XmlText]
        public string Url { get; set; }

        // Atribút width z elementu <image>
        [MaxLength(10)]
        [XmlAttribute("width")]
        public string? Width { get; set; }

        // Atribút height z elementu <image>
        [MaxLength(10)]
        [XmlAttribute("height")]
        public string? Height { get; set; }

        // Atribút alt z elementu <image>
        [MaxLength(200)]
        [XmlAttribute("alt")]
        public string? Alt { get; set; }
    }
}
