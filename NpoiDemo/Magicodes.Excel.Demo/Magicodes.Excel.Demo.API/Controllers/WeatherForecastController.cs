using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Magicodes.Excel.Demo.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        [HttpGet]
        //[Fact(DisplayName = "Excel模板导出教材订购明细样表")]
        public async Task ExportByTemplate_Test()
        {
            //模板路径
            var tplPath = Path.Combine(Directory.GetCurrentDirectory(), "XXXX运营账户名称交易详Template.xlsx");
            //创建Excel导出对象
            IExportFileByTemplate exporter = new ExcelExporter();
            //导出路径
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), nameof(ExportByTemplate_Test) + ".xlsx");
            //if (Files.Exists(filePath)) File.Delete(filePath);
            //根据模板导出
            await exporter.ExportByTemplate(filePath,
                new TextbookOrderInfo("湖南心莱信息科技有限公司", "湖南长沙岳麓区", "雪雁", "1367197xxxx", "雪雁", DateTime.Now.ToLongDateString(),
                    new List<BookInfo>()
                    {
                new BookInfo(1, "0000000001", "《XX从入门到放弃》", "张三", "机械工业出版社", "3.14", 100, "备注"),
                new BookInfo(2, "0000000002", "《XX从入门到放弃》", "张三", "机械工业出版社", "3.14", 100, "备注"),
                new BookInfo(3, "0000000003", "《XX从入门到放弃》", "张三", "机械工业出版社", "3.14", 100, "备注")
                    }),
                tplPath);


            ////模板路径
            ////public async Task<FileContentResult> ExportByTemplate_Test()
            //var tplPath = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "TransactionRecordTemplate.xlsx");
            //var filedata = await new ExcelExporter().ExportBytesByTemplate(new { Name = data.PlatformName, List = list }, tplPath);
            //return File(filedata.ToArray(), "application/octet-stream", $"{data.PlatformName}交易详情{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx");
        }
    }


    /// <summary>
    /// 教材订购信息
    /// </summary>
    public class TextbookOrderInfo
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string Company { get; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; }

        /// <summary>
        /// 制表人
        /// </summary>
        public string Watchmaker { get; }

        /// <summary>
        /// 时间
        /// </summary>
        public string Time { get; }

        /// <summary>
        /// 教材信息列表
        /// </summary>
        public List<BookInfo> BookInfos { get; }

        public TextbookOrderInfo(string company, string address, string contact, string tel, string watchmaker, string time, List<BookInfo> bookInfo)
        {
            Company = company;
            Address = address;
            Contact = contact;
            Tel = tel;
            Watchmaker = watchmaker;
            Time = time;
            BookInfos = bookInfo;
        }
    }



    /// <summary>
    /// 教材信息
    /// </summary>
    public class BookInfo
    {
        /// <summary>
        /// 行号
        /// </summary>
        public int RowNo { get; }

        /// <summary>
        /// 书号
        /// </summary>
        public string No { get; }

        /// <summary>
        /// 书名
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 主编
        /// </summary>
        public string EditorInChief { get; }

        /// <summary>
        /// 出版社
        /// </summary>
        public string PublishingHouse { get; }

        /// <summary>
        /// 定价
        /// </summary>
        public string Price { get; }

        /// <summary>
        /// 采购数量
        /// </summary>
        public int PurchaseQuantity { get; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; }

        public BookInfo(int rowNo, string no, string name, string editorInChief, string publishingHouse, string price, int purchaseQuantity, string remark)
        {
            RowNo = rowNo;
            No = no;
            Name = name;
            EditorInChief = editorInChief;
            PublishingHouse = publishingHouse;
            Price = price;
            PurchaseQuantity = purchaseQuantity;
            Remark = remark;
        }
    }




























}
