using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.AfiTravelModel
{
    public class Country : BaseEntity<short>
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override short Id { get; set; }

        public bool? IsNajziadanejsia { get; set; }

        [MaxLength(50)]
        public string CountryName { get; set; }
    }
}
