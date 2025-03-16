using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.AfiTravelModel
{
    public class Country : BaseEntity
    {
        [Key]
        [Required]
        public override Guid Id { get; set; } = Guid.NewGuid();

        public bool? IsNajziadanejsia { get; set; }

        [MaxLength(50)]
        public string CountryName { get; set; }
    }
}
