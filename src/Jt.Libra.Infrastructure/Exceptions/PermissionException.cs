using Jt.Libra.Infrastructure.Enums;
using System;

namespace Jt.Libra.Infrastructure.Exceptions
{
    public class PermissionException : JtException
    {
        public PermissionException(string error, Exception innerException) : base(error, ResultCodeEnum.NoPermission, innerException)
        {
        }
        public PermissionException(string error) : base(error, ResultCodeEnum.NoPermission)
        {
        }
        public PermissionException() : base("", ResultCodeEnum.NoPermission)
        {
        }
    }
}