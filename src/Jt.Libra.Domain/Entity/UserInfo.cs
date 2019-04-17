using System;
using System.Collections.Generic;
using System.Text;

namespace Jt.Libra.Domain.Entity
{
    public class UserInfo : CreateAndDeleteAuditEntity
    {
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string RealName { get; set; }
        public string IdCardNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Gender { get; set; }
    }
}