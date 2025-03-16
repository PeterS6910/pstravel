using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ImportXml.AfiTravelModel
{
    public class Hotel : BaseEntity
    {
        [Key]
        [Required]
        public override Guid Id { get; set; } = Guid.NewGuid();

        public Guid CountryId { get; set; }

        public Guid? LocalityId { get; set; }

        [StringLength(120)]
        public string Name { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public double Stars { get; set; }

        public double? Rating { get; set; }

        public int? RatingCount { get; set; }

    }
}
