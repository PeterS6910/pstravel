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
    public class OfferRepository : EntityRepository<Offer, Guid>
    {
        private readonly CurrencyRepository? _currencyRepository;
        private readonly FoodRepository? _foodRepository;
        private readonly TransportRepository? _transportRepository;
        private readonly OfferTourTypesRepository? _offerTourTypesRepository;
        public OfferRepository(DbContext context) : base(context)
        {
            _currencyRepository = null;
            _foodRepository = null;
            _transportRepository = null;
            _offerTourTypesRepository = null;
        }

        public OfferRepository(DbContext context, CurrencyRepository? currencyRepository, FoodRepository? foodRepository, TransportRepository? transportRepository, OfferTourTypesRepository? offerTourTypesRepository) : base(context)
        {
            _currencyRepository = currencyRepository;
            _foodRepository = foodRepository;
            _transportRepository = transportRepository;
            _offerTourTypesRepository = offerTourTypesRepository;
        }
        // Špecifická metóda pre získanie krajiny podľa názvu
        public async Task<Guid> GetIdOrCreateOfferAsync(XElement offer, short cestovkaId, Guid hotelId)
        {
            var url = offer.Element("url")?.Value;
            var ret = url?.Split('=', '.');
            var sOfferId = ret != null && ret.Count() > 1 ? ret[3] : string.Empty;
            short currencyId = 1;
            short foodId = 1;
            short transportationId = 1;
            var from = offer.Element("term")?.Element("from")?.Value;
            var to = offer.Element("term")?.Element("to")?.Value;
            var length = offer.Element("term")?.Element("length")?.Value;
            var discont = offer.Element("discount")?.Value;

            var currencyName = offer?.Element("price")?.Attribute("currency")?.Value;
            if (!string.IsNullOrEmpty(currencyName) && _currencyRepository != null)
            {
                currencyId = await _currencyRepository.GetIdOrCreateCurrencyByNameAsync(currencyName);                
                var foodName = offer?.Element("food")?.Value;
                if (!string.IsNullOrEmpty(foodName)  && _foodRepository != null)
                {
                    foodId = await _foodRepository.GetIdOrCreateByFoodNameAsync(foodName);
                }
                var transportationName = offer?.Element("transportation")?.Value;
                if (!string.IsNullOrEmpty(transportationName) && _transportRepository != null)
                {
                    transportationId = await _transportRepository.GetIdOrCreateTransportByNameAsync(transportationName);
                }
                var offerOld = await _context.Set<Offer>().FirstOrDefaultAsync(x => x.Url == url);
                Guid IdOfferOld = offerOld != null ? offerOld.Id : Guid.NewGuid();

                Offer newOffer = new Offer
                {
                    Id = IdOfferOld,
                    CestovkaId = cestovkaId,
                    HotelId = hotelId,
                    Price = double.TryParse(offer.Element("price")?.Value, out double price) ? price : 0,
                    Tax = double.TryParse(offer.Element("tax")?.Value, out double tax) ? tax : 0,
                    TotalPrice = double.TryParse(offer.Element("totalprice")?.Value, out double totalPrice) ? totalPrice : 0,
                    CurrencyId = currencyId,
                    FoodId = foodId,
                    TransportationId = transportationId,
                    Url = url,
                    SOfferId = sOfferId,
                    From = DateTime.TryParse(from, out DateTime fromOffer) ? fromOffer : DateTime.MinValue,
                    To = DateTime.TryParse(to, out DateTime toOffer) ? toOffer : DateTime.MinValue,
                    Length = int.TryParse(length, out int tolength) ? tolength : 0,
                    Discount = short.TryParse(discont, out short todiscont) ? todiscont : (short)0,
                };
                try
                {
                    await UpsertAsync(newOffer);
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
                var tourtypes = offer?.Element("tourtypes");
                if (tourtypes != null && tourtypes.HasElements)
                {
                    List<string> tourTypes = tourtypes.Elements("type").Select(x => x.Value).ToList();
                    await _offerTourTypesRepository.CreateTourTypesByOfferAsync(tourTypes, newOffer.Id);
                }
                return newOffer.Id;
            }


            return Guid.Empty;
        }

    }
}
