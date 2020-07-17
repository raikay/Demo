using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExceptionDemo.Exceptions
{
    public class KnownException : IKnownException
    {
        public string Message { get; private set; }

        public int Code { get; private set; }

        public object[] Data { get; private set; }

        public readonly static IKnownException Unknown = new KnownException { Message = "未知错误", Code = 9999 };

        public static IKnownException FromKnownException(IKnownException exception)
        {
            return new KnownException { Message = exception.Message, Code = exception.Code, Data = exception.Data };
        }
    }
}
