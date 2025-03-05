using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ImportXml.AfiTravelModel
{
    public class TaxDetails
    {
        [Key]
        public Guid OfferId { get; set; }

        private decimal tax;

        [XmlIgnore] // Skryje túto vlastnosť pred XML serializáciou
        public decimal Tax
        {
            get => tax;
            set => tax = value;
        }

        [XmlText] // Používa string ako textový obsah XML elementu
        public string PriceAsText
        {
            get => tax.ToString(System.Globalization.CultureInfo.InvariantCulture);
            set => tax = decimal.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var parsedValue)
                ? parsedValue
                : 0m; // Fallback na 0, ak sa nepodarí parsovať
        }

        // Atribút width z elementu <image>
        [MaxLength(5)]
        [XmlAttribute("currency")]
        public string? Currency { get; set; }
    }
}
