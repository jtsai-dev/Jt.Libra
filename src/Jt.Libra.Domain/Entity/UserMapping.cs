using System;
using System.Collections.Generic;
using System.Text;

namespace Jt.Libra.Domain.Entity
{
    public class UserMapping : CreateAuditEntity
    {
        public long? UserId { get; set; }
        public long? WxUserId { get; set; }
        public string OpenId { get; set; }
        public string AppId { get; set; }
    }
}