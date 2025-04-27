using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.AfiTravelModel
{
    public  class Locality : BaseEntity<int>
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; } 

        public short CountryId { get; set; }

        public int? ParentLocalityId { get; set; }

        [MaxLength(80)]
        public string LocalityName { get; set; }

    }
}
