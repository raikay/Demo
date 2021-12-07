using System;

namespace SignalR.Models.Contexts
{
    /// <summary>
    /// 用户细节
    /// </summary>
    public class Users
    {
            /// <summary>
            /// 连接ID
            /// </summary>
            public string ConnectionId { get; set; }
            /// <summary>
            /// 用户ID
            /// </summary>
            public string UserId { get; set; }
            /// <summary>
            /// 用户名
            /// </summary>
            public string UserName { get; set; }
            /// <summary>
            /// 用户部门
            /// </summary>
            public string DeptName { get; set; }
            /// <summary>
            /// 登录时间
            /// </summary>
            public DateTime LoginTime { get; set; }
        }
}