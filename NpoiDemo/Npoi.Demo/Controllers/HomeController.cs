using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {


            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet = book.CreateSheet();

            #region 定义表头

            //RGB自定义背景色
            HSSFPalette palette = book.GetCustomPalette();
            palette.SetColorAtIndex(HSSFColor.Pink.Index, (byte)54, (byte)96, (byte)146);


            HSSFCellStyle cellStyle = (HSSFCellStyle)book.CreateCellStyle();

            ///（自定义背景色）单元格背景颜色 和FillPattern必须一起设置
            cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Pink.Index;
            cellStyle.FillPattern = FillPattern.SolidForeground;

            cellStyle.Alignment = HorizontalAlignment.Center; //水平居中
            cellStyle.VerticalAlignment = VerticalAlignment.Center; //垂直居中

            //创建字体
            var cellStyleFont = (HSSFFont)book.CreateFont(); 
            cellStyleFont.Color = HSSFColor.White.Index;
            cellStyleFont.IsBold = true;
            cellStyleFont.FontName = "宋体";
            cellStyleFont.FontHeightInPoints = 11;
            cellStyle.SetFont(cellStyleFont);




            //设置单元格宽度
            sheet.SetColumnWidth(0, 4000);//核销人
            sheet.SetColumnWidth(1, 4000);//账号
            sheet.SetColumnWidth(2, 5000);//核销时间

            sheet.SetColumnWidth(3, 3500);
            sheet.SetColumnWidth(4, 3500);
            sheet.SetColumnWidth(5, 4000);
            sheet.SetColumnWidth(6, 7000);
            sheet.SetColumnWidth(7, 3500);//全面值
            sheet.SetColumnWidth(8, 4500);//使用门槛
            sheet.SetColumnWidth(9, 5000);//核销门店
            sheet.SetColumnWidth(10, 10000);//appid
            sheet.SetColumnWidth(11, 3500);//核销地址
            sheet.SetColumnWidth(12, 3500);//核销id

            // 第一行
            NPOI.SS.UserModel.IRow row = sheet.CreateRow(0);
            row.Height = 350;
            row.CreateCell(0).SetCellValue("核销人");

            row.CreateCell(1).SetCellValue("核销人账号");
            row.CreateCell(2).SetCellValue("核销时间");
            row.CreateCell(3).SetCellValue("订单金额");
            row.CreateCell(4).SetCellValue("核销金额");
            row.CreateCell(5).SetCellValue("核销券码");
            row.CreateCell(6).SetCellValue("有效期");
            row.CreateCell(7).SetCellValue("券面值");
            row.CreateCell(8).SetCellValue("使用门槛");
            row.CreateCell(9).SetCellValue("核销门店");
            row.CreateCell(10).SetCellValue("核销门店APPID");
            row.CreateCell(11).SetCellValue("核销地址");
            row.CreateCell(12).SetCellValue("核销ID");

            for (int i = 0; i < 13; i++)
            {
                row.Cells[i].CellStyle = cellStyle;
            }

            #endregion

            //合并单元格
            /**
               第一个参数：从第几行开始合并
               第二个参数：到第几行结束合并
               第三个参数：从第几列开始合并
               第四个参数：到第几列结束合并
             **/
            CellRangeAddress region = new CellRangeAddress(1, 2, 0, 0);
            sheet.AddMergedRegion(region);



            HSSFCellStyle cellStyleItem = (HSSFCellStyle)book.CreateCellStyle();

            cellStyleItem.Alignment = HorizontalAlignment.Center; //水平居中
            cellStyleItem.VerticalAlignment = VerticalAlignment.Center; //垂直居中

            HSSFCellStyle cellStyleItem2 = (HSSFCellStyle)book.CreateCellStyle();
            
            cellStyleItem2.VerticalAlignment = VerticalAlignment.Center; //垂直居中

            for (int i = 1; i < 100; i++)
            {
                NPOI.SS.UserModel.IRow row2 = sheet.CreateRow(i);
                row2.CreateCell(0).SetCellValue("收银员小吴");
                row2.CreateCell(1).SetCellValue("13587639864");
                row2.CreateCell(2).SetCellValue(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                row2.CreateCell(3).SetCellValue("1000");
                row2.CreateCell(4).SetCellValue("180");
                row2.CreateCell(5).SetCellValue("DJD84K6JFU");
                row2.CreateCell(6).SetCellValue($"{DateTime.Now.ToString("yyyy-MM-dd")} ~ {DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")}");
                row2.CreateCell(7).SetCellValue("60");
                row2.CreateCell(8).SetCellValue("满100元可用");
                row2.CreateCell(9).SetCellValue("世纪新园中餐厅");
                row2.CreateCell(10).SetCellValue(System.Guid.NewGuid().ToString());
                var ignoreList = new List<int> { 5, 8, 9, 10 };
                for (int j = 0; j < 11; j++)
                {
                    if (ignoreList.Contains(j)) 
                    {
                        continue;
                    }
                    row2.Cells[j].CellStyle = cellStyleItem;
                }
            }



            // 写入到客户端  
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("content-disposition", $"attachment;filename={DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xls;");

            Response.BinaryWrite(ms.ToArray());
            book = null;
            ms.Close();
            ms.Dispose();
            

            return null;
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}