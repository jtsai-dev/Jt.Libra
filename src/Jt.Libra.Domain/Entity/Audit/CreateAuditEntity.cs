using System;
using System.Collections.Generic;
using System.Text;

namespace Jt.Libra.Domain.Entity
{
    public abstract class CreateAuditEntity : BaseEntity
    {
        public virtual DateTime CreateTime { get; set; } = DateTime.Now;
        public virtual long? CreateBy { get; set; }
    }
}
