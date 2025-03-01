using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.AfiTravelModel
{
    public class Offer
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OffersId { get; set; } // Cudzie kľúčové pole
        public string Image { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public List<Photo> Photos { get; set; } = new List<Photo>();
        public Destination Destination { get; set; }

        [MaxLength(150)]
        public string Hotel { get; set; }
        public Term Term { get; set; }
        public decimal Price { get; set; }

        [MaxLength(20)]
        public string PriceCurrency { get; set; }
        public decimal Tax { get; set; }

        [MaxLength(20)]
        public string TaxCurrency { get; set; }
        public decimal TotalPrice { get; set; }

        [MaxLength(20)]
        public string TotalPriceCurrency { get; set; }
        public decimal Discount { get; set; }

        [MaxLength(50)]
        public string Food { get; set; }

        [MaxLength(50)]
        public string Transportation { get; set; }
        public List<Airports>  Airports { get; set; }

        [MaxLength(250)]
        public string Url { get; set; }
        public List<TourType> TourType { get; set; } = new List<TourType>();
        public HotelInfo HotelInfo { get; set; }
        public List<Actionattributes> Actionattributes { get; set; } = new List<Actionattributes>();

        [MaxLength(20)]
        public string TermType { get; set; }
    }
}
