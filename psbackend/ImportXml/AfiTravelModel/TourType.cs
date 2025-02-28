using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.AfiTravelModel
{
    public class TourType
    {
        [Key]
        public Guid Id { get; set; }
        public Guid OfferId { get; set; }
        public string Type { get; set; }
    }
}
