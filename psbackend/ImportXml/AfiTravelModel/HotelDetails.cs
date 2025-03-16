using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.AfiTravelModel
{
    public  class HotelDetails : BaseEntity
    {
        [Key]
        [Required]
        public override Guid Id { get; set; } = Guid.NewGuid();

        public Guid HotelId { get; set; }

        public Guid CestovkaId { get; set; }

        [StringLength(45)]
        public string IdHotelaCestovky { get; set; }

        public string? Description { get; set; }
    }
}
