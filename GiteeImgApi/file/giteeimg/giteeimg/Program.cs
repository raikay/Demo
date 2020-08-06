using giteeimg.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace giteeimg
{

    public class AddFileParam
    {
        public string access_token { set; get; }

        public string content { set; get; }

        public string message { set; get; }

        public string branch { set; get; }

    }

    class Program
    {
        static void Main(string[] args)
        {
            //{"access_token":"c1e9eba495a3db846846e7078a345493","name":"m1","has_issues":"true","has_wiki":"true","can_comment":"true","auto_init":"false","private":"false"}
            try
            {




                for (int i = 1; i < 9999999; i++)
                {
                    Random rd = new Random();
                    List<string> list = new List<string> { "20200405141837.jpg", "20200427210855.png" };
                    var imgPath = list.OrderBy(c => Guid.NewGuid()).FirstOrDefault();
                    DebugLogHelper.WriteLog("imgPath:" + imgPath);
                    Console.WriteLine("imgPath:" + imgPath);
                    var base64str = ReadFromFile(imgPath);
                    if (string.IsNullOrEmpty(base64str))
                    {
                        Console.WriteLine("转换base64失败");
                        Console.ReadKey();
                    }

                    DebugLogHelper.WriteLog($@"进入第{i}次循环");
                    int rNum = 1;//rd.Next(1, 1000);
                    AddFileParam data = new AddFileParam
                    {
                        access_token = "c1e9eba495a3db846846e7078a345493",
                        content = base64str,
                        message = "m" + rNum.ToString(),
                        branch = "master"

                    };
                    string jsonStr = JsonConvert.SerializeObject(data);
                    var dt = DateTime.Now.ToString("yyyyMMddHHmmsss");
                    string url = $@"https://gitee.com/api/v5/repos/imgrep001/m{rNum.ToString()}/contents/{dt}.jpg";
                    Console.WriteLine($@"第{i.ToString()}次 : {url}");
                    DebugLogHelper.WriteLog($@"请求Url:{url}");
                    var resultDataJson = HttpHelper.HttpPost(url, jsonStr);
                    DebugLogHelper.WriteLog($@"返回结果:{resultDataJson}");
                    Console.WriteLine(resultDataJson);
                    var resultData = JsonConvert.DeserializeObject<dynamic>(resultDataJson);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                DebugLogHelper.WriteLog($@"异常：" + ex.ToString());
                Console.ReadKey();
            }




            //创建仓库 
            try
            {


                for (int i = 7; i < 1000; i++)
                {

                    SaveReposParam data = new SaveReposParam
                    {
                        access_token = "6079355dd7e1f5e5981518b166a03cff",
                        name = "m" + i.ToString(),
                        has_issues = true,
                        has_wiki = true,
                        can_comment = true,
                        auto_init = false,
                        privateType = false

                    };
                    string jsonStr = JsonConvert.SerializeObject(data);
                    Console.WriteLine(i);
                    var resultDataJson = HttpHelper.HttpPost("https://gitee.com/api/v5/user/repos", jsonStr);
                    //Console.WriteLine(resultDataJson);
                    //var resultData = JsonConvert.DeserializeObject<dynamic>(resultDataJson);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            

            Console.ReadKey();
            Console.WriteLine("Hello World!");
        }



        private static string ReadFromFile(string path)
        {
            FileStream fsForRead = new FileStream(path, FileMode.Open);
            string base64Str = "";
            try
            {
                //读入一个字节
                //Console.Write("文件的第一个字节为：" + fsForRead.ReadByte().ToString());
                //Console.ReadLine();
                //读写指针移到距开头10个字节处
                fsForRead.Seek(0, SeekOrigin.Begin);
                byte[] bs = new byte[fsForRead.Length];
                int log = Convert.ToInt32(fsForRead.Length);
                //从文件中读取10个字节放到数组bs中
                fsForRead.Read(bs, 0, log);
                base64Str = Convert.ToBase64String(bs);
                return base64Str;
                //Console.Write("base64编码：" + base64Str);
                //Console.ReadLine();
            }
            catch (Exception ex)
            {
                DebugLogHelper.WriteLog("转换base64异常：" + ex.ToString());
                Console.Write(ex.Message);
                //Console.ReadLine();
                return base64Str; ;
            }
            finally
            {
                fsForRead.Close();
            }
        }
    }
}
