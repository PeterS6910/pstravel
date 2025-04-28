using ImportXml.AfiTravelModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.Repository
{
    public class FoodRepository : EntityRepository<Food, short>
    {
        public FoodRepository(DbContext context) : base(context)
        {
        }

        public async Task<short> GetIdOrCreateByFoodNameAsync(string foodName)
        {
            var food = await _context.Set<Food>().FirstOrDefaultAsync(c => c.FoodName == foodName);
            if (food == null)
            {
                food = new Food
                {
                    FoodName = foodName,
                    CreatedAt = DateTime.UtcNow,
                };
                var addedEntity = await _context.Set<Food>().AddAsync(food);
                await _context.SaveChangesAsync();
                return addedEntity.Entity.Id;                
            }
            return food.Id;
        }

    }
}
