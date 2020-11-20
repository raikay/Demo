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


            Dictionary<string, object> dataItem = new Dictionary<string, object>();
            dataItem.Add("ImportTime", "2020");
            dataItem.Add("StoreName", "门店");
            dataItem.Add("StoreAddress", "哒哒哒哒哒哒多多多多多多");
            dataItem.Add("Number", "5");
            dataItem.Add("UserNames", "张三");

            Dictionary<string, object> dataItem1 = new Dictionary<string, object>();
            dataItem1.Add("ImportTime", "202011");
            dataItem1.Add("StoreName", "门店11");
            dataItem1.Add("StoreAddress", "哒哒哒哒哒哒多多多多多多11");
            dataItem1.Add("Number", "511");
            dataItem1.Add("UserNames", "张三11");


            list.Add(dataItem);
            list.Add(dataItem1);




            data.Add("batchNo", $"No.{DateTime.Now.ToString("ddHHmmss")}");
            data.Add("noticeTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            data.Add("userName", "张三");
            data.Add("mx", list);
            data.Add("msg", "天冷穿衣");


            NpoiHeplper.Export("OffShelfInfoTemplate.doc", $"new{dt}.doc", data);
            Console.WriteLine("OK");
            Console.ReadKey();
        }
    }
}
