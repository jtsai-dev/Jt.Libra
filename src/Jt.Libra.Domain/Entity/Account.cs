using System;
using System.Collections.Generic;
using System.Text;

namespace Jt.Libra.Domain.Entity
{
    public class Account : CreateAndDeleteAuditEntity
    {
        public string AccountName { get; set; }
        public string NickName { get; set; }
        public string PhoneNumber { get; set; }
        public int? Gender { get; set; }
    }
}
