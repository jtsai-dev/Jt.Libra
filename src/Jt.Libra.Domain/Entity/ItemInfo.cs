using System;
using System.Collections.Generic;
using System.Text;

namespace Jt.Libra.Domain.Entity
{
    public class ItemInfo : FullAuditEntity
    {
        public long DirectoryId { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}