using ImportXml.AfiTravelModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.Repository
{
    public class OfferTourTypesRepository : EntityRepository<OfferTourTypes, Guid>
    {
        public OfferTourTypesRepository(DbContext context) : base(context)
        {
        }

        // Špecifická metóda pre získanie krajiny podľa názvu
        public async Task<Country?> GetCountryByNameAsync(string countryName)
        {
            return await _context.Set<Country>().FirstOrDefaultAsync(c => c.CountryName == countryName);
        }

        public async Task CreateTourTypesByOfferAsync(List<string> tourTypes, Guid offeId)
        {
            foreach(var typeName in tourTypes)
            {
                var type = await _context.Set<TourTypes>().FirstOrDefaultAsync(x=>x.TourTypesName == typeName);
                if(type != null)
                {
                    var offerTourTypes = await _context.Set<OfferTourTypes>().FirstOrDefaultAsync(c => c.OfferId == offeId && c.TourTypesId == type.Id);
                    if (offerTourTypes == null) 
                    {
                        OfferTourTypes newOfferTourTypes = new OfferTourTypes
                        {
                            Id = Guid.NewGuid(),
                            OfferId = offeId,
                            TourTypesId = type.Id,
                            CreatedAt = DateTime.UtcNow,
                        };
                        await AddAsync(newOfferTourTypes);
                    }
                }
            }
        }
    }
}
