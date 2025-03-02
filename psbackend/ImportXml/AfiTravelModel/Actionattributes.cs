using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ImportXml.AfiTravelModel
{
    public class Actionattributes
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OfferId { get; set; }

        [MaxLength(150)]  // Nastaví dĺžku VARCHAR na 50
        [XmlElement("attr")]
        public string Attr { get; set; }
    }
}
