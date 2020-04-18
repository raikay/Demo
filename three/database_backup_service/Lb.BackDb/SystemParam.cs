using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lb.BackDb
{
    public static class SystemParam
    {
        /// <summary>
        /// 配置文件
        /// </summary>
        public static readonly string ConfigFilePath = Path .Combine(AppDomain.CurrentDomain.BaseDirectory,"SystemConfig.xml");
    }
}
