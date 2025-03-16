using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.AfiTravelModel
{
    public class Transport : BaseEntity
    {
        [Key]
        public override Guid Id { get; set; } = Guid.NewGuid();

        public Guid? ParentTransportId { get; set; }

        [MaxLength(30)]
        [Required]
        public string TransportName { get; set; }
    }
}
