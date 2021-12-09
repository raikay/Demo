using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRDemo.Server;
using System.Threading.Tasks;

namespace SignalRDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHubContext<SignalRHub> _countHub;

        public HomeController(IHubContext<SignalRHub> countHub)
        {
            _countHub = countHub;
        }

        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Send(string msg, string id)
        {
            await _countHub.Clients.All.SendAsync("AddMsg", $"{id}：{msg}");
        }
    }
}
