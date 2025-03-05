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
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid CestovkaId { get; set; }

        public Guid OffersId { get; set; } // Cudzie kľúčové pole

        [XmlElement("image")]
        public Image Image { get; set; }

        [XmlArray("photos")] 
        [XmlArrayItem("photo")]
        public List<Photo> Photos { get; set; } = new List<Photo>();

        [XmlElement("destination")]
        public Destination Destination { get; set; }

        [XmlElement("hotel")]
        public string Hotel { get; set; }

        [XmlElement("term")]
        public Term Term { get; set; }

        [XmlElement("price")]
        public PriceDetails Price { get; set; }

        [XmlElement("tax")]
        public TaxDetails Tax { get; set; }

        [XmlElement("totalprice")]
        public TotalPriceDetails TotalPrice { get; set; }

        [XmlElement("discount")]
        public decimal Discount { get; set; }

        [XmlElement("food")]
        public string Food { get; set; }

        [XmlElement("transportation")]
        public string Transportation { get; set; }

        [XmlArray("airports")]
        [XmlArrayItem("airport")]
        public List<Airports> Airports { get; set; } = new List<Airports>();

        [XmlElement("url")]
        public string Url { get; set; }

        [XmlArray("tourtypes")]
        [XmlArrayItem("type")]
        public List<TourType> TourType { get; set; } = new List<TourType>();

        [XmlElement("hotelinfo")]
        public HotelInfo HotelInfo { get; set; }

        [XmlArray("actionattributes")]
        [XmlArrayItem("attr")]
        public List<Actionattributes> Actionattributes { get; set; } = new List<Actionattributes> { };

        [XmlElement("termtype")]
        public string TermType { get; set; }
    }
}