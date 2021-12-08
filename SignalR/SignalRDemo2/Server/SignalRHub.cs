using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SignalRDemo.Server
{
    public class SignalRHub : Hub
    {
        /// <summary>
        /// 客户连接成功时触发
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            var cid = Context.ConnectionId;

            //根据id获取指定客户端
            var client = Clients.Client(cid);

            //向指定用户发送消息
            await client.SendAsync("Self", cid);

            //像所有用户发送消息
            await Clients.All.SendAsync("AddMsg", $"{cid}加入了聊天室");
        }
    }
}