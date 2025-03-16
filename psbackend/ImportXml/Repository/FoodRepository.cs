using ImportXml.AfiTravelModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.Repository
{
    public class FoodRepository : EntityRepository<Food>
    {
        public FoodRepository(DbContext context) : base(context)
        {
        }

        public async Task<Guid> GetIdOrCreateByFoodNameAsync(string foodName)
        {
            var food = await _context.Set<Food>().FirstOrDefaultAsync(c => c.FoodName == foodName);
            if (food == null)
            {
                food = new Food
                {
                    Id = Guid.NewGuid(),
                    FoodName = foodName,
                    CreatedAt = DateTime.UtcNow,
                };
                await _context.AddAsync(food);
                await _context.SaveChangesAsync();
            }
            return food.Id;
        }

    }
}
