using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExceptionDemo.Exceptions
{
    public class MyServerException : Exception, IKnownException
    {
        public MyServerException(string message, int errorCode, params object[] errorData) : base(message)
        {
            this.Code = errorCode;
            this.Data = errorData;
        }

        public int Code { get; private set; }
        public object[] Data { get; private set; }
    }
}
