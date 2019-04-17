using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jt.Libra.WebApi.Models
{
    public class WechatApiConfig
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public bool Lifetime { get; set; }
        public bool IssuerSigningKey { get; set; }
        public double Duration { get; set; }
    }

    public class WechatApiConfigItem
    {
        public string Url { get; set; }

    }
}