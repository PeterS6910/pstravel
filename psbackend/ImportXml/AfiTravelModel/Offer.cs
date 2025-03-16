using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ImportXml.AfiTravelModel
{
    public class Offer : BaseEntity
    {
        [Key]
        public override Guid Id { get; set; } = Guid.NewGuid();

        public Guid CestovkaId { get; set; }

        public Guid HotelId { get; set; } // Cudzie kľúčové pole
              
        public Double Price { get; set; }
        
        public Double Tax { get; set; }

        public Double TotalPrice { get; set; }

        public Guid CurrencyId { get; set; }

        public Guid FoodId { get; set; }

        public Guid TransportationId { get; set; }

        public string Url { get; set; }

        [MaxLength(50)]
        public string SOfferId {  get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int Length { get; set; }

        [MaxLength(10)]
        public string Discount { get; set; }

    }
}