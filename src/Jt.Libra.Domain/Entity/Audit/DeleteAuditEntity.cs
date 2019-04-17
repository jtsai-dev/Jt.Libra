using System;
using System.Collections.Generic;
using System.Text;

namespace Jt.Libra.Domain.Entity
{
    public abstract class DeleteAuditEntity : BaseEntity
    {
        public virtual DateTime? DeleteTime { get; set; }
        public virtual long? DeleteBy { get; set; }
        public virtual bool IsDeleted { get; set; } = false;
    }
}
