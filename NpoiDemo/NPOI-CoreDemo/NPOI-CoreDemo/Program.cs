using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NPOI_CoreDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using System.IO.StreamReader reader = new System.IO.StreamReader("data.json");
            string contents = reader.ReadToEnd();
            reader.Close();
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<dynamic>>(contents);
            var ms = GetExcelMemoryStream(data);
            //Controller 直接返回
            //return File(ms.ToArray(), "application/octet-stream", string.Format("冷链食品处理详情{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmss")));
            //ms 保存文件

            using FileStream fileStream = new FileStream($"newfile{DateTime.Now.ToString("HHmmss")}.xls", FileMode.Create, FileAccess.Write);

            fileStream.Write(ms.ToArray());

            Console.WriteLine("OK");
            Console.ReadKey();
        }


private static MemoryStream GetExcelMemoryStream(List<dynamic> orderItemList)
{

    var first = orderItemList.FirstOrDefault();
    //创建表格
    NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
    NPOI.SS.UserModel.ISheet sheet = book.CreateSheet();

    #region 定义样式

    //创建字体
    var cellStyleFont = (HSSFFont)book.CreateFont();
    cellStyleFont.IsBold = true;
    cellStyleFont.FontName = "宋体";
    cellStyleFont.FontHeightInPoints = 10;


    //RGB自定义背景色
    HSSFPalette palette = book.GetCustomPalette();
    palette.SetColorAtIndex(HSSFColor.Pink.Index, (byte)220, (byte)220, (byte)220);


    // titleStyle
    HSSFCellStyle titleStyle = (HSSFCellStyle)book.CreateCellStyle();
    //（自定义背景色）单元格背景颜色 和FillPattern必须一起设置
    titleStyle.FillForegroundColor = HSSFColor.Pink.Index;
    titleStyle.FillPattern = FillPattern.SolidForeground;

    titleStyle.VerticalAlignment = VerticalAlignment.Center; //垂直居中
    titleStyle.BorderBottom = BorderStyle.Thin;
    titleStyle.BorderLeft = BorderStyle.Thin;
    titleStyle.BorderRight = BorderStyle.Thin;
    titleStyle.BorderTop = BorderStyle.Thin;
    titleStyle.SetFont(cellStyleFont);//设置字体 


    //headStyle
    HSSFCellStyle headStyle = (HSSFCellStyle)book.CreateCellStyle();
    headStyle.Alignment = HorizontalAlignment.Center; //水平居中
    headStyle.BorderBottom = BorderStyle.Thin;
    headStyle.BorderLeft = BorderStyle.Thin;
    headStyle.BorderRight = BorderStyle.Thin;
    headStyle.BorderTop = BorderStyle.Thin;
    headStyle.SetFont(cellStyleFont);

    //borderStyle
    HSSFCellStyle borderStyle = (HSSFCellStyle)book.CreateCellStyle();
    borderStyle.BorderBottom = BorderStyle.Thin;
    borderStyle.BorderLeft = BorderStyle.Thin;
    borderStyle.BorderRight = BorderStyle.Thin;
    borderStyle.BorderTop = BorderStyle.Thin;
    #endregion

    #region 表头
    //设置单元格宽度
    sheet.SetColumnWidth(0, 5000);//采购日期
    sheet.SetColumnWidth(1, 5000);//门店名称
    sheet.SetColumnWidth(2, 14000);//门店地址

    sheet.SetColumnWidth(3, 4500);//采购数量
    sheet.SetColumnWidth(4, 4500);//操作人

    //表头
    NPOI.SS.UserModel.IRow row = sheet.CreateRow(4);
    row.Height = 350;
    row.CreateCell(0).SetCellValue("采购日期");

    row.CreateCell(1).SetCellValue("门店名称");
    row.CreateCell(2).SetCellValue("门店地址");
    row.CreateCell(3).SetCellValue("采购数量");
    row.CreateCell(4).SetCellValue("操作人");
    for (int i = 0; i < 5; i++)
    {
        row.Cells[i].CellStyle = titleStyle;
    }
    //合并单元格
    CellRangeAddress region0 = new CellRangeAddress(0, 0, 0, 4);
    sheet.AddMergedRegion(region0);


    IRow row0 = sheet.CreateRow(0);
    row0.CreateCell(0).SetCellValue("冷链食品处理详情");
    row0.Cells[0].CellStyle = headStyle;
    var row0Cell4 = row0.CreateCell(4);
    row0Cell4.CellStyle = headStyle;

    IRow row1 = sheet.CreateRow(1);
    row1.CreateCell(0).SetCellValue("处理商品批次码");
    row1.CreateCell(1).SetCellValue(first.BatchNo.ToString());

    row1.Cells[0].CellStyle = titleStyle;

    IRow row2 = sheet.CreateRow(2);
    row2.CreateCell(0).SetCellValue("处理日期");
    row2.CreateCell(1).SetCellValue(first.NoticeTime?.ToString("yyyy.MM.dd"));
    row2.Cells[0].CellStyle = titleStyle;

    IRow row3 = sheet.CreateRow(3);
    row3.CreateCell(0).SetCellValue("处理人");
    row3.CreateCell(1).SetCellValue(first.NoticeUserName.ToString());
    row3.Cells[0].CellStyle = titleStyle;

    //设置黑色边框
    for (int i = 1; i < 4; i++)
    {
        var iRow = sheet.GetRow(i);
        for (int j = 1; j < 5; j++)
        {
            var jCell = (j == 1) ? iRow.GetCell(j) : iRow.CreateCell(j);
            jCell.CellStyle = borderStyle;
        }
    }


    #endregion

    #region 循环数据

    int index = 5;
    foreach (var item in orderItemList)
    {
        IRow itemRow = sheet.CreateRow(index);

        var rowCell0 = itemRow.CreateCell(0);
        rowCell0.SetCellValue((item.ImportTime ?? item.SubTime).ToString("yyyy.MM.dd"));
        rowCell0.CellStyle = borderStyle;

        var rowCell1 = itemRow.CreateCell(1);
        rowCell1.SetCellValue(item.TargetStoreName.ToString());
        rowCell1.CellStyle = borderStyle;


        var rowCell2 = itemRow.CreateCell(2);
        rowCell2.SetCellValue(item.TargetStoreAddress.ToString());
        rowCell2.CellStyle = borderStyle;

        var rowCell3 = itemRow.CreateCell(3);
        rowCell3.SetCellValue(item.Number?.ToString());
        rowCell3.CellStyle = borderStyle;

        var rowCell4 = itemRow.CreateCell(4);
        rowCell4.SetCellValue(item.ImportUserNames.ToString());
        rowCell4.CellStyle = borderStyle;


        index++;
    }

    #endregion

    MemoryStream ms = new MemoryStream();
    book.Write(ms);
    return ms;
}
    }
}
