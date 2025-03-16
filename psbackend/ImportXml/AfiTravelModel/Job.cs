using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.AfiTravelModel
{
    public class Job : BaseEntity
    {

        [Key]
        public override Guid Id { get; set; } = Guid.NewGuid();

        [MaxLength(30)]
        public string Name { get; set; }

        public Guid JobCodeId { get; set; }

        public Guid JobStateId { get; set; }

        public DateTime ScheduledTime { get; set; }

        public string InputParameters { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }
    }
}
