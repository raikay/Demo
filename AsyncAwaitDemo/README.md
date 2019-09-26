
使用Result,请求的主线程（threedId=1）会阻塞等待异步线程（threedId=4）执行完毕，主线程继续后面的工作。  

不使用Resutl 遇到await异步方法，主线程会去做其他事情，异步线程执行完毕，返回后由异步线程做后面的工作，  

避免了线程切换的开销。  

代码如下：
```
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = new Per().GetStrAsyc().Result;
            result = new Kaper().GetStr();
        }


    }

    public class Per
    {
        public async Task<string> GetStrAsyc()
        {
            //threedId=1
            var threedId = Thread.CurrentThread.ManagedThreadId.ToString();
            var result = await GetJsonAsyc();
            //threedId=4
            threedId = Thread.CurrentThread.ManagedThreadId.ToString();
            return result;
        }

        public async Task<string> GetJsonAsyc()
        {
            return await Task.Run(() =>
            {
                //threedId=4
                var threedId = Thread.CurrentThread.ManagedThreadId.ToString();
                return "result";

            });
        }
    }

    public class Kaper
    {
        public string GetStr()
        {
            //threedId=1
            var threedId = Thread.CurrentThread.ManagedThreadId.ToString();
            var result = GetJsonAsyc().Result;
            //threedId=1
            threedId = Thread.CurrentThread.ManagedThreadId.ToString();
            return result;
        }

        public async Task<string> GetJsonAsyc()
        {
            return await Task.Run(() =>
            {
                //threedId=4
                var threedId = Thread.CurrentThread.ManagedThreadId.ToString();
                return "result";

            });
        }
    }
}

```
