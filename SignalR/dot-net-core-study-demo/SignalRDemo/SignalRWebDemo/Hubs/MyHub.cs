using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRWebDemo.Hubs
{
    public class MyHub:Hub
    {
        /// <summary>
        /// 这个方法允许客户端调用
        /// </summary>
        public Task UpdateDataServer(DataStatus data)
        {
            // 在里面可以调用客户端的方法
            return Clients.All.SendAsync("UpdateData", data);
        }
    }
    public class DataStatus
    {
        public string Status1 { get; set; }
        public string Status2 { get; set; }
    }
    public class MsgInfo
    {
        public string Title { get; set; }
        public string MsgContent { get; set; }
    }
}
