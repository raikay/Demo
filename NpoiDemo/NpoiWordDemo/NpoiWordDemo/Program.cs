using System;
using System.Collections.Generic;

namespace NpoiWordDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var dt = DateTime.Now.ToString("yyyyMMddHHmmss");

            Dictionary<string, object> data = new Dictionary<string, object>();

            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            Dictionary<string, object> item1 = new Dictionary<string, object>();
            item1.Add("NTime", "2020-11-21");
            item1.Add("StoreName", "先南小吃");
            item1.Add("Addres", "北京昌平区中街211");
            item1.Add("Num", "5");
            item1.Add("Name", "杨过");

            Dictionary<string, object> item2 = new Dictionary<string, object>();
            item2.Add("NTime", "2020-11-05");
            item2.Add("StoreName", "龙女包子");
            item2.Add("Addres", "中原古墓左手边");
            item2.Add("Num", "511");
            item2.Add("Name", "小龙女");

            Dictionary<string, object> item3 = new Dictionary<string, object>();
            item3.Add("NTime", "2020-05-02");
            item3.Add("StoreName", "欧阳锋麻辣烫");
            item3.Add("Addres", "中原沙河镇5路");
            item3.Add("Num", "200");
            item3.Add("Name", "欧阳锋");


            Dictionary<string, object> item4 = new Dictionary<string, object>();
            item4.Add("NTime", "2020-05-02");
            item4.Add("StoreName", "丐帮饭馆");
            item4.Add("Addres", "中原丐帮地带7街8号");
            item4.Add("Num", "50");
            item4.Add("Name", "老顽童");

            list.Add(item1);
            list.Add(item2);
            list.Add(item3);
            list.Add(item4);

            List<Dictionary<string, object>> xqList = new List<Dictionary<string, object>>();

            Dictionary<string, object> xqitem1 = new Dictionary<string, object>();
            xqitem1.Add("Name", "小龙虾");
            xqitem1.Add("Num", "100");

            Dictionary<string, object> xqitem2 = new Dictionary<string, object>();
            xqitem2.Add("Name", "皮皮虾");
            xqitem2.Add("Num", "200");
            xqList.Add(xqitem1);
            xqList.Add(xqitem2);

            data.Add("batchNo", $"No.{DateTime.Now.ToString("ddHHmmss")}");
            data.Add("noticeTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            data.Add("userName", "张三");
            data.Add("ApprovalDate", DateTime.Now.ToString("yyyy-MM-dd"));
            data.Add("mx", list);
            data.Add("xq", xqList);

            NpoiHeplper.Export("OffShelfInfoTemplate.doc", $"new{dt}.doc", data);
            Console.WriteLine("OK");
            Console.ReadKey();
        }
    }
}
