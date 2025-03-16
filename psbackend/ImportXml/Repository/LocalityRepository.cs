using ImportXml.AfiTravelModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.Repository
{
    public class LocalityRepository : EntityRepository<Locality>
    {
        public LocalityRepository(DbContext context) : base(context)
        {
        }

        public async Task<Locality?> GetLocalityByNameAsync(string localityName)
        {
            return await _context.Set<Locality>().FirstOrDefaultAsync(c => c.LocalityName == localityName);
        }

        public async Task<Guid> GetIdOrCreateLocalityByNameAsync(Guid countryId, string localityName)
        {
            var locality = await _context.Set<Locality>().FirstOrDefaultAsync(l => l.LocalityName == localityName && l.CountryId == countryId);
            if (locality == null)
            {
                locality = new Locality
                {
                    Id = Guid.NewGuid(),
                    LocalityName = localityName,
                    CountryId = countryId,
                    CreatedAt = DateTime.UtcNow
                };

                await _context.Set<Locality>().AddAsync(locality);
                await _context.SaveChangesAsync();
            }

            return locality.Id;
        }
    }
}
