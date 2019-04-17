using Jt.Libra.Infrastructure.Enums;
using System;

namespace Jt.Libra.Infrastructure.Exceptions
{
    public class ValidationException : JtException
    {
        public ValidationException(string error, Exception innerException) : base(error, ResultCodeEnum.UnValid, innerException)
        {
        }
        public ValidationException(string error) : base(error, ResultCodeEnum.UnValid)
        {
        }
        public ValidationException() : base("", ResultCodeEnum.UnValid)
        {
        }
    }
}