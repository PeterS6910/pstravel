using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ImportXml.AfiTravelModel
{
    public class Offer : BaseEntity <Guid>
    {
        [Key]
        public override Guid Id { get; set; } = Guid.NewGuid();

        public short CestovkaId { get; set; }

        public Guid HotelId { get; set; } // Cudzie kľúčové pole
              
        public Double Price { get; set; }
        
        public Double Tax { get; set; }

        public Double TotalPrice { get; set; }

        public short CurrencyId { get; set; }

        public short FoodId { get; set; }

        public short TransportationId { get; set; }

        public string Url { get; set; }

        [MaxLength(50)]
        public string SOfferId {  get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int Length { get; set; }

        public short Discount { get; set; }

    }
}