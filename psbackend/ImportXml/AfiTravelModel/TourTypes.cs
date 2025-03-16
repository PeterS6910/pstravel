using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.AfiTravelModel
{
    public class TourTypes : BaseEntity
    {
        [Key]
        public override Guid Id { get; set; } = Guid.NewGuid();

        [StringLength(30)]
        public string TourTypesName { get; set; }
    }
}
