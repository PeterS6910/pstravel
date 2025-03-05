using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ImportXml.AfiTravelModel
{
    public class Cestovka
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [MaxLength(80)]
        public string Nazov { get; set; }

        [MaxLength(10)]
        public string ICO { get; set; }

        [MaxLength(50)]
        public string? Kontakt { get; set; }

        [MaxLength(50)]
        public string? Email { get; set; }
    }
}
