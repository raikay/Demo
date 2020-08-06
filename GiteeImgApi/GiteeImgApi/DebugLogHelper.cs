
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


public class DebugLogHelper
{
    private static readonly object obj = new object();

    /// <summary>
    /// 日志记录
    /// </summary>
    /// <param name="message">记录信息</param>
    /// <param name="businessName">业务名称</param>
    /// <param name="logPath">保存路径</param>
    public static void WriteLog(string message, string businessName = "综合记录", string logPath = "D:\\Logs")
    {

        lock (obj)
        {
            DateTime dt = DateTime.Now;
            message = "\r\n" + dt.ToString() + ":\r\n" + message + "\r\n";
            string pPath = string.Format("{0}\\{1}\\{2}", logPath, dt.ToString("yyyy-MM-dd"), businessName);
            if (!Directory.Exists(pPath))
            {
                Directory.CreateDirectory(pPath);
            }

            string aName = string.Format("{0}\\{1}.log", pPath, dt.Hour);
            using (FileStream myFileStream = new FileStream(aName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            {
                byte[] byteArr = Encoding.Default.GetBytes(message);
                myFileStream.Write(byteArr, 0, byteArr.Length);
                myFileStream.Close();
            }
        }

    }
}
