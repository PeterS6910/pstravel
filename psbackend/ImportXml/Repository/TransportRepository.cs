using ImportXml.AfiTravelModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.Repository
{
    public class TransportRepository : EntityRepository<Transport>
    {
        public TransportRepository(DbContext context) : base(context)
        {
        }

        public async Task<Guid> GetIdOrCreateTransportByNameAsync(string transportName)
        {
            var transport = await _context.Set<Transport>().FirstOrDefaultAsync(c => c.TransportName == transportName);
            if (transport == null)
            {
                transport = new Transport
                {
                    Id = Guid.NewGuid(),
                    TransportName = transportName,
                    ParentTransportId = null,
                    CreatedAt = DateTime.UtcNow,
                };
                await _context.Set<Transport>().AddAsync(transport);
                await _context.SaveChangesAsync();
            }
            return transport.Id;
        }

    }
}
