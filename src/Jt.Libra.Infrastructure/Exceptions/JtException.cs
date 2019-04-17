using Jt.Libra.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jt.Libra.Infrastructure.Exceptions
{
    public class JtException : Exception
    {
        public ResultCodeEnum Code { get; }

        public JtException(string error, ResultCodeEnum code, Exception innerException) : base(error, innerException)
        {
            Code = code;
        }

        public JtException(string error, ResultCodeEnum code) : base(error)
        {
            Code = code;
        }

        public JtException(string error) : this(error, ResultCodeEnum.UnknownError)
        {
        }
    }
}
