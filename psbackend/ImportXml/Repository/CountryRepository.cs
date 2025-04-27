using ImportXml.AfiTravelModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.Repository
{
    public class CountryRepository : EntityRepository<Country, short>
    {
        public CountryRepository(DbContext context) : base(context)
        {
        }

        // Špecifická metóda pre získanie krajiny podľa názvu
        public async Task<Country?> GetCountryByNameAsync(string countryName)
        {
            return await _context.Set<Country>().FirstOrDefaultAsync(c => c.CountryName == countryName);
        }

        public async Task<short> GetOrCreateCountryByNameAsync(string countryName)
        {
            if (string.IsNullOrEmpty(countryName))
            {
                return short.MinValue;
            }

            var country = await _context.Set<Country>().FirstOrDefaultAsync(c => c.CountryName == countryName);
            if (country == null)
            {
                country = new Country
                {
                    IsNajziadanejsia = false,
                    CountryName = countryName,
                    CreatedAt = DateTime.UtcNow
                };

                var addedEntity = await _context.Set<Country>().AddAsync(country);
                await _context.SaveChangesAsync();
                return addedEntity.Entity.Id;
            }

            return country.Id;
        }
    }

}
