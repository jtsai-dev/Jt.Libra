using System;
using System.Collections.Generic;
using System.Text;

namespace Jt.Libra.Domain.Entity
{
    public class WxUserInfo : UpdateAuditEntity
    {
        public string UnionId { get; set; }
        public string NickName { get; set; }
        public string Avatar { get; set; }
        public int? Gender { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
    }
}