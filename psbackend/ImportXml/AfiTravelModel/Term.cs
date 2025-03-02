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
    public class Term
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OfferId { get; set; }

        [XmlElement("from")]
        public DateTime From { get; set; }

        [XmlElement("to")]
        public DateTime To { get; set; }

        [XmlElement("length")]
        public int Length { get; set; }
    }
}
