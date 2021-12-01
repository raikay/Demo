### async result await

使用Result,请求的主线程（threedId=1）会阻塞等待异步线程（threedId=4）执行完毕，主线程继续后面的工作。  

不使用Resutl 遇到await异步方法，主线程会去做其他事情，异步工作执行完毕，返回后由其他线程接管后面的工作，  没有线程阻塞

async方法内用await是将异步变为方法内多线程伪造的同步。整个函数相对于调用者来说依然是异步，而且await后的全部工作交给其他线程。

多线程伪造同步：看着是同步的，也可以拿到执行结果，实际是异步工作执行完毕，由其他线程接管当前线的程工作。

### 死锁问题
Framwork中不能使用result,必锁死。  

Core中Result阻塞编程，高并发时会造成各种死锁问题，没有更好的解决办法。  

所以推荐 始终async/await  

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

![IMG](https://gitee.com/imgrep001/m1/raw/master/2021/12/01/20211201041935.png)

来自网友：

> 1、只要涉及IO操作（比如访问数据库，调用web api），就要尽可能使用 asyncawait
> 2、使用async/await这种异步写法比起同步的优势就是降低服务器压力
> https://www.jianshu.com/p/4768c954a85f?tdsourcetag=s_pctim_aiomsg

2021年3月2日

简单总结：

加入async await 和同步方法在执行顺序上没有什么区别，对于服务器来说，减轻压力，线程由线程池掌控，不会出现同步的阻塞，遇到await就会释放当前主线程