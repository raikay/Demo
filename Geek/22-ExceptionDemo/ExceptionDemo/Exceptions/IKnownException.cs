using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExceptionDemo.Exceptions
{
    public interface IKnownException
    {
        public string Message { get; }

        public int Code { get; }

        public object[] Data { get; }
    }
}
