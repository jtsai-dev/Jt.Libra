using System;
using System.Collections.Generic;
using System.Text;

namespace Jt.Libra.Application.Log
{
    public class LogInput
    {
        public string Token { get; set; }

        public string Url { get; set; }

        public string Input { get; set; }

        public string Output { get; set; }

        public DateTime ServerTime { get { return DateTime.Now; } }
    }
}
