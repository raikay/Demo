using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using Lb.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lb.Tests
{
    [TestClass]
    public class MyTest
    {
        [TestMethod]
        public void CreateZipTests()
        {
            string path = "D:\\DataBase\\BackupDB\\DBTEST";
            DateTime ftime = DateTime.Now.AddDays(-1500);
            for (int i = 0; i < 1500; i++)
            {
                using (ZipFile zip = ZipFile.Create(Path.Combine(path, $"{ftime.ToString("yyyy-MM-dd")}.zip")))
                {
                    zip.BeginUpdate();
                    //添加文件 
                    zip.Add("F:\\压缩测试.txt");
                    zip.CommitUpdate();
                }
                ftime = ftime.AddDays(1);
            }

            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                FileInfo f = new FileInfo(file);
                f.CreationTime = Path.GetFileNameWithoutExtension(f.Name).ObjToDateTime();
            }

        }
    }
}
