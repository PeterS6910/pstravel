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
    public class OfferRepository : EntityRepository<Offer>
    {
        private readonly CurrencyRepository? _currencyRepository;
        private readonly FoodRepository? _foodRepository;
        private readonly TransportRepository? _transportRepository;
        public OfferRepository(DbContext context) : base(context)
        {
            _currencyRepository = null;
            _foodRepository = null;
            _transportRepository = null;
        }

        public OfferRepository(DbContext context, CurrencyRepository? currencyRepository, FoodRepository? foodRepository, TransportRepository? transportRepository) : base(context)
        {
            _currencyRepository = currencyRepository;
            _foodRepository = foodRepository;
            _transportRepository = transportRepository;
        }
        // Špecifická metóda pre získanie krajiny podľa názvu
        public async Task<Guid> GetIdOrCreateOfferAsync(XElement offer, Guid cestovkaId, Guid hotelId)
        {
            var url = offer.Element("url")?.Value;
            var ret = url?.Split('=', '.');
            var sOfferId = ret != null && ret.Count() > 1 ? ret[3] : string.Empty;
            Guid currencyId = Guid.Empty;
            Guid foodId = Guid.Empty;
            Guid transportationId = Guid.Empty;
            var from = offer.Element("term")?.Element("from")?.Value;
            var to = offer.Element("term")?.Element("to")?.Value;
            var length = offer.Element("term")?.Element("length")?.Value;
            if (!string.IsNullOrEmpty(sOfferId))
            {
                var offerOld = await _context.Set<Offer>().FirstOrDefaultAsync(x => x.Url == url);
                if (offerOld == null)
                {
                    var currencyName = offer?.Element("price")?.Attribute("currency")?.Value;
                    if (!string.IsNullOrEmpty(currencyName))
                    {
                        var currency = await _context.Set<Currency>().FirstOrDefaultAsync(x => x.CurrencyName == currencyName);
                        if (currency != null)
                        {
                            currencyId = currency.Id;
                        }
                        var foodName = offer?.Element("food")?.Value;
                        if (!string.IsNullOrEmpty(foodName))
                        {
                            var foodFind = await _context.Set<Food>().FirstOrDefaultAsync(x => x.FoodName == foodName);
                            if (foodFind != null)
                            {
                                foodId = foodFind.Id;
                            }
                        }

                        var transportationName = offer?.Element("transportation")?.Value;
                        if (!string.IsNullOrEmpty(transportationName))
                        {
                            var transportationFind = await _context.Set<Transport>().FirstOrDefaultAsync(x => x.TransportName == transportationName);
                            if (transportationFind != null)
                            {
                                transportationId = transportationFind.Id;

                            }
                            else
                            {
                                Transport newTransport = new Transport
                                {
                                    Id = Guid.NewGuid(),
                                    ParentTransportId = null,
                                    TransportName = transportationName,
                                    CreatedAt = DateTime.UtcNow,
                                };
                                await _context.Set<Transport>().AddAsync(newTransport);
                                await _context.SaveChangesAsync();
                                transportationId = newTransport.Id;
                            }
                        }

                        //var TourTypesZoznam = await getTourtypes(offer?.Element("tourtypes"));
                        Offer newOffer = new Offer
                        {
                            Id = Guid.NewGuid(),
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
                            Discount = offer.Element("discount")?.Value ?? string.Empty,
                        };
                        try
                        {
                            await _context.Set<Offer>().AddAsync(newOffer);
                            await _context.SaveChangesAsync();
                            return newOffer.Id;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message.ToString());
                        }
                    }
                }
                else
                {
                    return offerOld.Id;
                }
            }
            return Guid.Empty;
        }

    }
}
