using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.AfiTravelModel
{
    public class Coords
    {
        [Key]
        public Guid Id { get; set; }
        public Guid HotelInfoId { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
