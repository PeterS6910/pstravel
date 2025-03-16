using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.AfiTravelModel
{
    public class OfferTourTypes : BaseEntity
    {
        [Key]
        public override Guid Id { get; set; } = Guid.NewGuid();

        public Guid OfferId { get; set; }

        public Guid TourTypesId { get; set; }
    }
}
