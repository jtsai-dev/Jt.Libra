using System;
using System.Collections.Generic;
using System.Text;

namespace Jt.Libra.Domain.Entity
{
    public abstract class BaseEntity<T>
    {
        public virtual T Id { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<long>
    {

    }
}
