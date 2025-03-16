using ImportXml.AfiTravelModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.Repository
{
    public class CountryRepository : EntityRepository<Country>
    {
        public CountryRepository(DbContext context) : base(context)
        {
        }

        // Špecifická metóda pre získanie krajiny podľa názvu
        public async Task<Country?> GetCountryByNameAsync(string countryName)
        {
            return await _context.Set<Country>().FirstOrDefaultAsync(c => c.CountryName == countryName);
        }

        public async Task<Guid> GetOrCreateCountryByNameAsync(string countryName)
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
