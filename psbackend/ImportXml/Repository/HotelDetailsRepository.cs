using ImportXml.AfiTravelModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.Repository
{
    public class HotelDetailsRepository : EntityRepository<HotelDetails, Guid>
    {
        public HotelDetailsRepository(DbContext context) : base(context)
        {
        }

        public async Task<Guid> GetOrCreateHotelDetailsByHotelIdCestovkaIdAsync(Guid hotelId, short cestovkaId, string IdHotelaCestovky)
        {
            if (string.IsNullOrEmpty(IdHotelaCestovky))
            {
                return Guid.Empty;
            }

            var hotelDetails = await _context.Set<HotelDetails>().FirstOrDefaultAsync(x => x.HotelId == hotelId && x.CestovkaId == cestovkaId);
            Guid IdHotelDetails = hotelDetails == null ? Guid.NewGuid() : hotelDetails.Id;

            hotelDetails = new HotelDetails
            {
                Id = IdHotelDetails,
                HotelId = hotelId,
                CestovkaId = cestovkaId,
                IdHotelaCestovky = IdHotelaCestovky,
                CreatedAt = DateTime.Now
            };
            await UpsertAsync(hotelDetails);            

            return hotelDetails.Id;
        }
    }
}
