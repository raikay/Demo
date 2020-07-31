using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class TimeSpanEnum
{
    /// <summary>
    /// 1个月
    /// </summary>
    public static TimeSpan OneMonth
    {
        get
        {
            return new TimeSpan(7, 0, 0);
        }
    }
    /// <summary>
    /// 1周
    /// </summary>
    public static TimeSpan OneWeek
    {
        get
        {
            return new TimeSpan(7, 0, 0);
        }
    }
    /// <summary>
    /// 1天
    /// </summary>
    public static TimeSpan OneDay
    {
        get
        {
            return new TimeSpan(1, 0, 0);
        }
    }
    /// <summary>
    /// 1小时
    /// </summary>
    public static TimeSpan OneHour
    {
        get
        {
            return new TimeSpan(0, 1, 0);
        }
    }
    /// <summary>
    /// 1分钟
    /// </summary>
    public static TimeSpan OneMinute
    {
        get
        {
            return new TimeSpan(0, 0, 1);
        }
    }
    /// <summary>
    /// 30秒
    /// </summary>
    public static TimeSpan ThirtySecond
    {
        get
        {
            return new TimeSpan(0, 0, 0, 30, 0);
        }
    }
    /// <summary>
    /// 1秒
    /// </summary>
    public static TimeSpan OneSecond
    {
        get
        {
            return new TimeSpan(0, 0, 0, 30, 0);
        }
    }
}
