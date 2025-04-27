using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ImportXml.AfiTravelModel
{
    public  class Images : BaseEntity<Guid>
    {
        [Key]
        public override Guid Id { get; set; } = Guid.NewGuid();

        public Guid HotelId { get; set; }

        [MaxLength(150)]
        public string Url { get; set; }

        [MaxLength(10)]
        public string? Width { get; set; }

        // Atribút height z elementu <image>
        [MaxLength(10)]
        public string? Height { get; set; }

        // Atribút alt z elementu <image>
        [MaxLength(200)]
        public string? Alt { get; set; }
    }
}
