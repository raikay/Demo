using System;
using System.Collections.Generic;
using System.Text;

namespace AutofacDemo.Service
{

    public class Services : IServices
    {

        public List<string> GetDataList()
        {
            return new List<string> { "result1", "result2" };
        }
    }
}
