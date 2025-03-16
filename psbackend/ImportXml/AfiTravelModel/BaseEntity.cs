using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.AfiTravelModel
{
    public abstract class BaseEntity : IEntity
    {
        public virtual Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Automaticky nastaví dátum vytvorenia
        public DateTime? UpdatedAt { get; set; } // Nullable - môže byť null, ak nebol aktualizovaný
        public DateTime? DeletedAt { get; set; } // Nullable - ak je null, záznam nie je zmazaný
    }
}
