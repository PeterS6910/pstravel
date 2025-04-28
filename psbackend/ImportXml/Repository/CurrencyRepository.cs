using ImportXml.AfiTravelModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.Repository
{
    public class CurrencyRepository : EntityRepository<Currency, short>
    {
        public CurrencyRepository(DbContext context) : base(context)
        {
        }

        public async Task<short> GetIdOrCreateCurrencyByNameAsync(string currencyName)
        {
            if (string.IsNullOrEmpty(currencyName))
            {
                return 0;
            }
            var currency = await _context.Set<Currency>().FirstOrDefaultAsync(c => c.CurrencyName == currencyName);
            if (currency == null)
            {
                currency = new Currency
                {                    
                    CurrencyName = currencyName,
                    CreatedAt = DateTime.UtcNow,
                };
                var addedEntity = await _context.Set<Currency>().AddAsync(currency);
                await _context.SaveChangesAsync();
                return addedEntity.Entity.Id;
            }
            return currency.Id;
        }

    }
}
