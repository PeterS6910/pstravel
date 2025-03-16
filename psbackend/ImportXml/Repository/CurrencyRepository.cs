using ImportXml.AfiTravelModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.Repository
{
    public class CurrencyRepository : EntityRepository<Currency>
    {
        public CurrencyRepository(DbContext context) : base(context)
        {
        }

        public async Task<Guid> GetIdOrCreateCurrencyByNameAsync(string currencyName)
        {
            if (string.IsNullOrEmpty(currencyName))
            {
                return Guid.Empty;
            }
            var currency = await _context.Set<Currency>().FirstOrDefaultAsync(c => c.CurrencyName == currencyName);
            if (currency == null)
            {
                currency = new Currency
                {
                    Id = Guid.NewGuid(),
                    CurrencyName = currencyName,
                    CreatedAt = DateTime.UtcNow,
                };
                await _context.Set<Currency>().AddAsync(currency);
                await _context.SaveChangesAsync();
            }
            return currency.Id;
        }

    }
}
