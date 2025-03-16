using ImportXml.AfiTravelModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.Repository
{
    public class HotelDetailsRepository : EntityRepository<HotelDetails>
    {
        public HotelDetailsRepository(DbContext context) : base(context)
        {
        }
        
        public async Task<Guid> GetOrCreateHotelDetailsByHotelIdCestovkaIdAsync(Guid hotelId, Guid cestovkaId, string IdHotelaCestovky)
        {
            if (string.IsNullOrEmpty(IdHotelaCestovky))
            {
                return Guid.Empty;
            }

            var hotelDetails = await _context.Set<HotelDetails>().FirstOrDefaultAsync(x => x.HotelId == hotelId && x.CestovkaId == cestovkaId);
            if (hotelDetails == null)
            {
                hotelDetails = new HotelDetails
                {
                    Id = Guid.NewGuid(),
                    HotelId = hotelId,
                    CestovkaId = cestovkaId,
                    IdHotelaCestovky = IdHotelaCestovky,
                    CreatedAt = DateTime.Now
                };

                await _context.Set<HotelDetails>().AddAsync(hotelDetails);
                await _context.SaveChangesAsync();
            }
            return hotelDetails.Id;
        }
    }
}
