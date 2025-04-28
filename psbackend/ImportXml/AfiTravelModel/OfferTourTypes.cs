using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.AfiTravelModel
{
    public class OfferTourTypes : BaseEntity<Guid>
    {
        [Key]
        [Required]
        public override Guid Id { get; set; } 

        public Guid OfferId { get; set; }

        public short TourTypesId { get; set; }
    }
}
