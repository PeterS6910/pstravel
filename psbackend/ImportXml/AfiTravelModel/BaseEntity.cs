using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportXml.AfiTravelModel
{
    public abstract class BaseEntity<T> : IEntity<T>
    {
        public virtual T Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Automaticky nastaví dátum vytvorenia
        public DateTime? UpdatedAt { get; set; } // Nullable - môže byť null, ak nebol aktualizovaný
        public DateTime? DeletedAt { get; set; } // Nullable - ak je null, záznam nie je zmazaný
    }
}
