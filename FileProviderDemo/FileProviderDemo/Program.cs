using Microsoft.Extensions.FileProviders;
using System;

namespace FileProviderDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 获取物理文件
            //Microsoft.Extensions.FileProviders.Physical

            ////定义物理文件应用程序， 映射当前程序运行目录
            IFileProvider provider1 = new PhysicalFileProvider(AppDomain.CurrentDomain.BaseDirectory);

            ////获取目录下所有内容
            //var contents = provider1.GetDirectoryContents("/");


            //foreach (var item in contents)
            //{
            //    //var stream = item.CreateReadStream();//创建文件流;

            //    Console.WriteLine(item.Name);
            //}


            #endregion


            #region 嵌入式资源文件
            //Microsoft.Extensions.FileProviders.Embedded

            IFileProvider provider2 = new EmbeddedFileProvider(typeof(Program).Assembly);


            //var html = provider2.GetFileInfo("emb.html");

            #endregion

            #region 组合文件提供程序
            //把嵌入式资源 和物理资源 组合到一个目录
            //Microsoft.Extensions.FileProviders.Composite

            IFileProvider provider = new CompositeFileProvider(provider1, provider2);



            var contents = provider.GetDirectoryContents("/");


            foreach (var item in contents)
            {
                Console.WriteLine(item.Name);
            }
            #endregion

            Console.ReadKey();
        }
    }
}
