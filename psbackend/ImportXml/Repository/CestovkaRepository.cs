using ImportXml.AfiTravelModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.Repository
{
    public class CestovkaRepository : EntityRepository<Cestovka>
    {
        public CestovkaRepository(DbContext context) : base(context)
        {
        }

        public async Task<Guid> GetIdByCestovkaNameAsync(string cestovkaName)
        {
            var cestovka = await _context.Set<Cestovka>().FirstOrDefaultAsync(c => c.Nazov == cestovkaName);
            if (cestovka == null)
            {
                cestovka = new Cestovka
                {
                    Id = Guid.NewGuid(),
                    Nazov = cestovkaName,
                    CreatedAt = DateTime.UtcNow,
                };
                await _context.Set<Cestovka>().AddAsync(cestovka);
                await _context.SaveChangesAsync();
            }
            return cestovka.Id;
        }
       
    }
}
