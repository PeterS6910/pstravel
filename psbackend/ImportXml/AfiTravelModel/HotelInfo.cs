using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.AfiTravelModel
{
    public class HotelInfo
    {
        [Key]
        public Guid HotelInfoId { get; set; } = Guid.NewGuid();
        public Guid OfferId { get; set; }

        [MaxLength(30)]
        public string Id { get; set; }
        public int Stars { get; set; }
        public double? Rating { get; set; } // Nullable, lebo nie vždy je prítomné
        public int? RatingCount { get; set; } // Nullable, lebo nie vždy je prítomné
        public Coords Coords { get; set; }

        [MaxLength(150)]
        public string Url { get; set; }
    }
}
