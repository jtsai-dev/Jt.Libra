using System;
using System.Collections.Generic;
using System.Text;

namespace Jt.Libra.Application.DirectoryInfo
{
    public class DirectoryTree
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Logitude { get; set; }
        public double Latitude { get; set; }
        public DateTime CreateTime { get; set; }
        public List<DirectoryTree> Children { get; set; }
    }
}
