using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GiteeImgApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {

        //Nlog构造方法注入
        private readonly ILogger<FileController> _logger;

        public FileController(ILogger<FileController> logger)
        {
            _logger = logger;
        }
        public class AddFileParam
        {
            public string access_token { set; get; }

            public string content { set; get; }

            public string message { set; get; }

            public string branch { set; get; }

        }
        [HttpGet]
        public IActionResult Get()
        {
            return Content("helloworld");
        }
            [HttpPost]
        public IActionResult Post(IFormFile file)
        {
            var r = ReadFromFile(file.OpenReadStream());
            //var s = file;
            var base64str = r;
            if (string.IsNullOrEmpty(base64str))
            {
                Console.WriteLine("转换base64失败");
                Console.ReadKey();
            }

            //DebugLogHelper.WriteLog($@"进入第{i}次循环");
            int rNum = 1;//rd.Next(1, 1000);
            AddFileParam data = new AddFileParam
            {
                access_token = "c1e9eba495a3db846846e7078a345493",
                content = base64str,
                message = "m" + "1",
                branch = "master"

            };
            string jsonStr = JsonConvert.SerializeObject(data);
            var dt = DateTime.Now.ToString("yyyyMMddHHmmsss");
            var fileExt = file.FileName.Split('.')[1];
            string url = $@"https://gitee.com/api/v5/repos/imgrep001/m{rNum.ToString()}/contents/{dt}.{fileExt}";



            _logger.LogInformation(JsonConvert.SerializeObject(new { RequestUrl = url }));

            var resultDataJson = HttpHelper.HttpPost(url, jsonStr);

            _logger.LogInformation("返回结果"+resultDataJson);

            Console.WriteLine(resultDataJson);
            var resultData = JsonConvert.DeserializeObject<dynamic>(resultDataJson);
            //if (file == null || !file.IsValid)
            //return new JsonResult(new { code = 500, message = "不允许上传的文件类型" });

            string newFile = string.Empty;
            //if (file != null)
            //   newFile = await file.SaveAs("/data/files/images");
            string reurl = resultData.content.download_url;
            return new JsonResult(new { url = reurl, code = 1, msg = "上传成功" });
        }


        private  string ReadFromFile(Stream fsForRead)
        {
            //FileStream fsForRead = new FileStream(path, FileMode.Open);
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
                _logger.LogError("转换base64异常：" + ex.ToString());
                
                Console.Write(ex.Message);
                //Console.ReadLine();
                return base64Str; ;
            }
            finally
            {
                fsForRead.Close();
            }
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete()
        {
            int i = 5;
            string strUrl = $"https://gitee.com/api/v5/repos/imgrep001/m{i}?access_token=c1e9eba495a3db846846e7078a345493";

            HttpHelper.HttpDel(strUrl);


            return null;
        }




    }


}
