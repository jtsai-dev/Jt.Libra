using System;
using System.Collections.Generic;
using System.Text;

namespace Jt.Libra.Domain.Entity
{
    public class InvokeRecord : CreateAuditEntity
    {
        public long DirectoryInfoId { get; set; }
        public long ItemInfoId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
