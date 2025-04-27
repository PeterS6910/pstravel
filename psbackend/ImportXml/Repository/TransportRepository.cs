using ImportXml.AfiTravelModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.Repository
{
    public class TransportRepository : EntityRepository<Transport, short>
    {
        public TransportRepository(DbContext context) : base(context)
        {
        }

        public async Task<short> GetIdOrCreateTransportByNameAsync(string transportName)
        {
            var transport = await _context.Set<Transport>().FirstOrDefaultAsync(c => c.TransportName == transportName);
            if (transport == null)
            {
                transport = new Transport
                {
                    TransportName = transportName,
                    ParentTransportId = null,
                    CreatedAt = DateTime.UtcNow,
                };
                var addedEntity = await _context.Set<Transport>().AddAsync(transport);
                await _context.SaveChangesAsync();
                return addedEntity.Entity.Id;
            }
            return transport.Id;
        }

    }
}
