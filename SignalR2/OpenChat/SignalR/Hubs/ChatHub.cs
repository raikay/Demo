using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SignalR.Models.Contexts;
using SignalR.Models.Http;

namespace SignalR.Hubs
{
    [HubName("chat")]
    public class ChatHub : Hub
    {
        public static List<Users> ConnectedUsers =new List<Users>();
        public void Send(string clientName,string message)
        {
            
            Clients.User(clientName).newLog();
            //解析用户信息的类容，去做相对应的处理
           message = UrlInfo.Get(message);

            Clients.All.addNewMessageToPage(clientName, message);
            //Clients.All.hello();
        }
    }
}