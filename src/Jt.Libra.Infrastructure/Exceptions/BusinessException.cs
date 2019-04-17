using Jt.Libra.Infrastructure.Enums;
using System;

namespace Jt.Libra.Infrastructure.Exceptions
{
    public class BussinessException : JtException
    {
        public BussinessException(string error, Exception innerException) : base(error, ResultCodeEnum.BussinessError, innerException)
        {
        }
        public BussinessException(string error) : base(error, ResultCodeEnum.BussinessError)
        {
        }
        public BussinessException() : base("", ResultCodeEnum.BussinessError)
        {
        }
    }
}