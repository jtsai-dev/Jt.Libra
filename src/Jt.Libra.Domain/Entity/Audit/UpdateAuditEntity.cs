using System;
using System.Collections.Generic;
using System.Text;

namespace Jt.Libra.Domain.Entity
{
    public abstract class UpdateAuditEntity : CreateAuditEntity
    {
        public virtual DateTime? UpdateTime { get; set; }
        public virtual long? UpdateBy { get; set; }
    }
}
