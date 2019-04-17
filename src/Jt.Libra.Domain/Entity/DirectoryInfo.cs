using System;
using System.Collections.Generic;
using System.Text;

namespace Jt.Libra.Domain.Entity
{
    public class DirectoryInfo : FullAuditEntity
    {
        public long UserId { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public double Logitude { get; set; }
        public double Latitude { get; set; }
    }
}
