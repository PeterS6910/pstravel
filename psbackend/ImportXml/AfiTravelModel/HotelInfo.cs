using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.AfiTravelModel
{
    public class HotelInfo
    {
        [Key]
        public Guid Id { get; set; }
        public Guid OfferId { get; set; }        
        public int Stars { get; set; }
        public double? Rating { get; set; } // Nullable, lebo nie vždy je prítomné
        public int? RatingCount { get; set; } // Nullable, lebo nie vždy je prítomné
        public Coords Coords { get; set; }
        public string Url { get; set; }
    }
}
