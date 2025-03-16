using ImportXml.AfiTravelModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.Repository
{
    public class TourTypesRepository : EntityRepository<TourTypes>
    {
        public TourTypesRepository(DbContext context) : base(context)
        {
        }

        public async Task<Guid> GetIdOrCreateTourTypesByNameAsync(string tourTypesName)
        {
            var tourTypes = await _context.Set<TourTypes>().FirstOrDefaultAsync(c => c.TourTypesName == tourTypesName);
            if (tourTypes == null)
            {
                tourTypes = new TourTypes
                {
                    Id = Guid.NewGuid(),
                    TourTypesName = tourTypesName,
                    CreatedAt = DateTime.UtcNow,
                };
                await _context.Set<TourTypes>().AddAsync(tourTypes);
                await _context.SaveChangesAsync();
            }
            return tourTypes.Id;

        }

    }
}
