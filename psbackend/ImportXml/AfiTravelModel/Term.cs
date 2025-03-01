using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.AfiTravelModel
{
    public class Term
    {
        [Key]
        public Guid Id { get; set; }
        public Guid OfferId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int Length { get; set; }
    }
}
