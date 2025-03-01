using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.AfiTravelModel
{
    public class Offer
    {
        [Key]
        public Guid Id { get; set; }
        public Guid OffersId { get; set; } // Cudzie kľúčové pole
        public string Image { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public List<Photo> Photo { get; set; } = new List<Photo>();
        public Destination Destination { get; set; }
        public string Hotel { get; set; }
        public Term Term { get; set; }
        public decimal Price { get; set; }
        public string PriceCurrency { get; set; }
        public decimal Tax { get; set; }
        public string TaxCurrency { get; set; }
        public decimal TotalPrice { get; set; }
        public string TotalPriceCurrency { get; set; }
        public decimal Discount { get; set; }
        public string Food { get; set; }
        public string Transportation { get; set; }
        public string Url { get; set; }
        public List<TourType> TourType { get; set; } = new List<TourType>();
        public HotelInfo HotelInfo { get; set; }
        public string TermType { get; set; }
    }
}
