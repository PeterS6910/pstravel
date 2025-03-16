using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.AfiTravelModel
{
    public  class Locality : BaseEntity
    {
        [Key]
        [Required]
        public override Guid Id { get; set; } = Guid.NewGuid();

        public Guid CountryId { get; set; }

        public Guid? ParentLocalityId { get; set; }

        [MaxLength(80)]
        public string LocalityName { get; set; }

    }
}
