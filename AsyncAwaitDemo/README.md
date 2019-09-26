### async result await

使用Result,请求的主线程（threedId=1）会阻塞等待异步线程（threedId=4）执行完毕，主线程继续后面的工作。  

不使用Resutl 遇到await异步方法，主线程会去做其他事情，异步线程执行完毕，返回后由异步线程做后面的工作，  

async方法内用，await是将异步变为方法内的同步。整个函数相对于调用者来说依然是异步，而且await后的全部工作交给另一个线程，且节省线程切换开销。

### 死锁问题
Framwork中不能使用result,必锁死。  

Core中Result阻塞编程，高并发时会造成各种死锁问题，没有更好的解决办法。  
  
所以推荐 始终Async  

[推荐阅读:Async/Await FAQ](https://devblogs.microsoft.com/pfxteam/asyncawait-faq/)  

[译文](https://www.cnblogs.com/heyuquan/p/async-deadlock.html)

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
