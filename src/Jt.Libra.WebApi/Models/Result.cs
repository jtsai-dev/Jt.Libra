using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jt.Libra.WebApi.Models
{
    public class Result<T>
    {
        public T Data { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public DateTime ServerTime => DateTime.Now;

        public Result(T data)
        {
            Data = data;
        }
        public Result()
        {

        }
    }

    public class Result : Result<object>
    {
        public Result(int code, string message = "")
        {
            Code = code;
            Message = message;
        }
    }
}
