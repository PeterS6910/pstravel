using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ImportXml.AfiTravelModel
{
    public class Offer
    {
        [Key]
        public Guid Id { get; set; }
        public Guid OffersId { get; set; } // Cudzie kľúčové pole

        [XmlElement("image")]
        public string Image { get; set; }

        [XmlAttribute("width")]
        public int ImageWidth { get; set; }

        [XmlAttribute("height")]
        public int ImageHeight { get; set; }

        [XmlElement("photos")]
        public List<Photo> Photo { get; set; } = new List<Photo>();

        [XmlElement("destination")]
        public Destination Destination { get; set; }

        [XmlElement("hotel")]
        public string Hotel { get; set; }

        [XmlElement("term")]
        public Term Term { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlAttribute("currency")]
        public string PriceCurrency { get; set; }

        [XmlElement("tax")]
        public decimal Tax { get; set; }

        [XmlAttribute("currency")]
        public string TaxCurrency { get; set; }

        [XmlElement("totalprice")]
        public decimal TotalPrice { get; set; }

        [XmlElement("currency")]
        public string TotalPriceCurrency { get; set; }

        [XmlElement("term")]
        public decimal Discount { get; set; }
        
        [XmlElement("food")]
        public string Food { get; set; }

        [XmlElement("transportation")]
        public string Transportation { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }

        [XmlElement("tourtypes")]
        public List<TourType> TourType { get; set; } = new List<TourType>();

        [XmlElement("hotelinfo")]
        public HotelInfo HotelInfo { get; set; }


        [XmlElement("termtype")]
        public string TermType { get; set; }
    }
}
