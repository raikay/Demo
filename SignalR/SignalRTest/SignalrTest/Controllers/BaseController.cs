using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//工具 -> 库程序包管理器 -> 程序包管理器控制台 输入下面命令  
//install-package Microsoft.AspNet.SignalR -Version 1.1.4  
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalR.Controllers
{

    //所有 hub  
    public class AllHub : Hub
    {

        /// <summary>
        /// 连接的用户数
        /// </summary>
        public static List<string> Users = new List<string>();

        /// <summary>
        /// 通知客户端
        /// </summary>
        public void SendSingle()
        {
            // Call the addNewMessageToPage method to update clients.
            var context = GlobalHost.ConnectionManager.GetHubContext<AllHub>();
        }

        /// <summary>
        /// Sends the update user count to the listening view.
        /// </summary>
        /// <param name="count">
        /// The count.
        /// </param>
        public void Send(int count)
        {
            // Call the addNewMessageToPage method to update clients.
            var context = GlobalHost.ConnectionManager.GetHubContext<AllHub>();
            context.Clients.All.updateUsersOnlineCount(count);
        }

        /// <summary>
        /// The OnConnected event.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override System.Threading.Tasks.Task OnConnected()
        {
            string clientId = GetClientId();

            if (Users.IndexOf(clientId) == -1)
            {
                Users.Add(clientId);
            }

            // Send the current count of users
            Send(Users.Count);

            var context = GlobalHost.ConnectionManager.GetHubContext<AllHub>();
            context.Clients.Client(clientId).updateUserName(clientId);


            return base.OnConnected();
        }

        /// <summary>
        /// The OnReconnected event.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override System.Threading.Tasks.Task OnReconnected()
        {
            string clientId = GetClientId();
            if (Users.IndexOf(clientId) == -1)
            {
                Users.Add(clientId);
            }

            // Send the current count of users
            Send(Users.Count);

            return base.OnReconnected();
        }

        /// <summary>
        /// The OnDisconnected event.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override System.Threading.Tasks.Task OnDisconnected()
        {
            string clientId = GetClientId();

            if (Users.IndexOf(clientId) > -1)
            {
                Users.Remove(clientId);
            }

            // Send the current count of users
            Send(Users.Count);

            return base.OnDisconnected();
        }

        /// <summary>
        /// 得到的当前连接的客户端ID.
        /// 这是每个客户独特的、用于识别一个连接
        /// </summary>
        /// <returns>The client Id.</returns>
        private string GetClientId()
        {
            string clientId = "";
            if (Context.QueryString["clientId"] != null)
            {
                // clientId passed from application 
                clientId = this.Context.QueryString["clientId"];
            }

            if (string.IsNullOrEmpty(clientId.Trim()))
            {
                clientId = Context.ConnectionId;
            }

            return clientId;
        }
    }

    //当前 hub  
    public class CurHub : Hub
    {
        public void SetRecGroup(string id)//设置接收组  
        {
            this.Groups.Add(this.Context.ConnectionId, id);
        }
    }

    [HubName("ChatRoomHub")]
    public class ChatHub : Hub
    {
        static List<UserEntity> users = new List<UserEntity>();

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="nickName"></param>
        public void UserEnter(string nickName)
        {
            UserEntity userEntity = new UserEntity
            {
                NickName = nickName,
                ConnectionId = Context.ConnectionId
            };

            users.Add(userEntity);
            Clients.All.NotifyUserEnter(nickName, users);//调用前台NotifyUserEnter方法
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="nickName"></param>
        /// <param name="message"></param>
        public void SendMessage(string nickName, string message)
        {
            Clients.All.NotifySendMessage(nickName, message);//调用前台NotifySendMessage方法
        }

        /// <summary>
        /// 断开（刷新页面可以触发此方法）
        /// </summary>
        /// <returns></returns>
        public override Task OnDisconnected()
        {
            var currentUser = users.FirstOrDefault(u => u.ConnectionId == Context.ConnectionId);
            if (currentUser != null)
            {
                users.Remove(currentUser);
                Clients.Others.NotifyUserLeft(currentUser.NickName, users);//调用前台NotifyUserLeft方法
            }
            return base.OnDisconnected();
        }
    }

    public class UserEntity
    {
        public string NickName { get; set; }

        public string ConnectionId { get; set; }
    }


    public class BaseController : Controller
    {
        /// <summary>
        /// 消息推送页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ProgressBar()
        {
            return View();
        }

        /// <summary>
        /// 进度条
        /// </summary>
        /// <returns></returns>
        public ActionResult Broadcast()
        {
            return View();
        }

        /// <summary>
        /// 聊天室
        /// </summary>
        /// <returns></returns>
        public ActionResult BroadcastTest()
        {
            return View();
        }

        //进度条  
        public void fnProgressBar()
        {
            for (int i = 0; i < 100; i++)
            {
                IHubContext chat = GlobalHost.ConnectionManager.GetHubContext<CurHub>();
                chat.Clients.Group("clientId").notify(i);//向指定组发送  
                System.Threading.Thread.Sleep(100);
            }
        }

        //广播  
        public string fnBroadcast(string msg)
        {
            string result = "发送失败!";
            try
            {
                IHubContext chat = GlobalHost.ConnectionManager.GetHubContext<AllHub>();
                chat.Clients.All.notice(msg);//向所有组发送  
                result = "发送成功!";
            }
            catch (Exception e)
            {
                result = "发送失败!\n失败原因:\n" + e.Message;
            }
            return result;
        }

    }
}