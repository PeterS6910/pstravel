using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.AfiTravelModel
{
    public class Offers
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public int Count { get; set; }
        public List<Offer> Offer { get; set; } = new List<Offer>();
    }
}
