using Jt.Libra.Infrastructure.Enums;
using System;

namespace Jt.Libra.Infrastructure.Exceptions
{
    public class AuthorizationException : JtException
    {
        public AuthorizationException(string error, Exception innerException) : base(error, ResultCodeEnum.Unauthorized, innerException)
        {
        }
        public AuthorizationException(string error) : base(error, ResultCodeEnum.Unauthorized)
        {
        }
        public AuthorizationException() : base("", ResultCodeEnum.Unauthorized)
        {
        }
    }
}