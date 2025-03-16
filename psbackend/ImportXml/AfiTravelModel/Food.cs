using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.AfiTravelModel
{
    public class Food : BaseEntity
    {
        [Key]
        public override Guid Id { get; set; } = Guid.NewGuid();

        [StringLength(40)]
        public string FoodName { get; set; }
    }
}
