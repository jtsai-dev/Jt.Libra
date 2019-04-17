using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Jt.Libra.Core.Security
{
    public class JtClaimTypes
    {
        public static string UserType { get; set; } = ClaimTypes.Role;

        public static string Id { get; set; } = ClaimTypes.NameIdentifier;

        public static string Mobile { get; set; } = ClaimTypes.MobilePhone;

        public static string AccountName { get; set; } = ClaimTypes.Name;

        public static string Gender { get; set; } = ClaimTypes.Gender;
    }
}