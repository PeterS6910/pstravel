using ImportXml.AfiTravelModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ImportXml.Repository
{
    public class HotelRepository : EntityRepository<Hotel>
    {
        private readonly HotelDetailsRepository? _hotelDetaisRepository;

        public HotelRepository(DbContext context) : base(context)
        {
            _hotelDetaisRepository = null;
        }

        public HotelRepository(DbContext context, HotelDetailsRepository hotelDetaisRepository)
               : base(context)
        {
            _hotelDetaisRepository = hotelDetaisRepository;
        }

        public async Task<Hotel?> GetOrCreateHotelByNameCountryLocalityAsync(string hotelName, XElement offer, Guid countryId, Guid? localityId, Guid cestovkaId)
        {
            if (string.IsNullOrEmpty(hotelName))
            {
                return null;
            }
            if (!string.IsNullOrEmpty(hotelName))
            {
                var hotelInfoId = offer.Element("hotelinfo")?.Element("id")?.Value;
                var hotel = await _context.Set<Hotel>().FirstOrDefaultAsync(x => x.CountryId == countryId && x.Name == hotelName && x.LocalityId == localityId);
                if (hotel == null)
                {
                    hotel = new Hotel
                    {
                        Id = Guid.NewGuid(),
                        CountryId = countryId,
                        LocalityId = localityId,
                        Name = hotelName,
                        Latitude = double.TryParse(offer.Element("hotelinfo")?.Element("coords")?.Element("lat").Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture,
                             out double lat) ? lat : 0.0,
                        Longitude = double.TryParse(offer.Element("hotelinfo")?.Element("coords")?.Element("lng").Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture,
                             out double lng) ? lng : 0.0,
                        Stars = double.TryParse(offer.Element("hotelinfo")?.Element("stars")?.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture,
                             out double stars) ? stars : 0.0,
                        Rating = double.TryParse(offer.Element("hotelinfo")?.Element("rating")?.Value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture,
                             out double rating) ? rating : 0.0,
                        RatingCount = int.TryParse(offer.Element("hotelinfo")?.Element("ratingcount")?.Value, out int ratingCount) ? ratingCount : 0,
                        CreatedAt = DateTime.Now
                    };
                    await _context.Set<Hotel>().AddAsync(hotel);
                    await _context.SaveChangesAsync();
                }

                if (!string.IsNullOrEmpty(hotelInfoId) && _hotelDetaisRepository != null)
                {
                    var hotelCestovkaId = await _hotelDetaisRepository.GetOrCreateHotelDetailsByHotelIdCestovkaIdAsync(hotel.Id, cestovkaId, hotelInfoId);
                }
                return hotel;

            }
            else return null;
        }

        public async Task<Guid> GetOrCreateCountryIdAsync(string countryName)
        {
            if (string.IsNullOrEmpty(countryName))
            {
                return Guid.Empty;
            }

            var country = await _context.Set<Country>().FirstOrDefaultAsync(c => c.CountryName == countryName);
            if (country == null)
            {
                country = new Country
                {
                    Id = Guid.NewGuid(),
                    CountryName = countryName,
                    CreatedAt = DateTime.UtcNow
                };

                await _context.Set<Country>().AddAsync(country);
                await _context.SaveChangesAsync();
            }

            return country.Id;
        }
    }
}
