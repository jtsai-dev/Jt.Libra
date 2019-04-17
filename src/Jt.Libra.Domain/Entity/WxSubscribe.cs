using System;
using System.Collections.Generic;
using System.Text;

namespace Jt.Libra.Domain.Entity
{
    public class WxSubscribe : BaseEntity
    {
        public long UserId { get; set; }
        public long WxUserId { get; set; }
        public string AppId { get; set; }

        public bool IsSubscribed { get; set; }
        public DateTime? SubscribeTime { get; set; }
        public DateTime? CancelSubscribeTime { get; set; }
    }
}