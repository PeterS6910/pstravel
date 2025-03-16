using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ImportXml.AfiTravelModel
{
    public class JobState : BaseEntity
    {
        [Key]
        public override Guid Id { get; set; } = Guid.NewGuid();

        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(30)]
        public string Code { get; set; }
    }
}
