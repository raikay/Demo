## NPOI 根据模板导出 word文档

### 文档模板：

![模板图片](https://gitee.com/imgrep001/m1/raw/master/2020/11/21/20201121183654.png)

单一字段替换：\#batchNo#  

需要循环的表格：$mx.NTime$  

### 生成后文档：

![IMG](https://gitee.com/imgrep001/m1/raw/master/2020/11/21/20201121183828.png)



### 调用方式：

```c#
NpoiHeplper.Export("OffShelfInfoTemplate.doc", $"new{dt}.doc", data);
```

data数据为字典格式

```
var dt = DateTime.Now.ToString("yyyyMMddHHmmss");
Dictionary<string, object> data = new Dictionary<string, object>();

List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

Dictionary<string, object> item1 = new Dictionary<string, object>();
item1.Add("NTime", "2020-11-21");
item1.Add("StoreName", "先南小吃");
item1.Add("Addres", "北京昌平区中街211");
item1.Add("Num", "5");
item1.Add("Name", "杨过");
list.Add(item1);

// new item2 item3 item4...
//list.Add(item2);
//list.Add(item3);
//list.Add(item4);
//new  xqList

data.Add("batchNo", $"No.{DateTime.Now.ToString("ddHHmmss")}");
data.Add("noticeTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
data.Add("userName", "张三");
data.Add("ApprovalDate", DateTime.Now.ToString("yyyy-MM-dd"));
data.Add("mx", list);
//data.Add("xq", xqList);
```

### 主要代码：

```c#
/// <summary>
/// 输出模板word文档
/// </summary>
/// <param name="tempFilePath">docx文件路径</param>
/// <param name="outPath">输出文件路径</param>
/// <param name="data">字典数据源</param>
public static void Export(string tempFilePath, string outPath, Dictionary<string, object> data)
{
    using (FileStream stream = File.OpenRead(tempFilePath))
    {
        XWPFDocument doc = new XWPFDocument(stream);
        //遍历段落                  
        foreach (var para in doc.Paragraphs)
        {
            ReplaceKey(para, data);
        }
        //遍历表格      
        foreach (var table in doc.Tables)
        {

            var ti = GetTableInfo(table, data);
            if (ti.IsList)
            {
                for (int i = 0; i < ti.Data.Count; i++)
                {
                    var nRow = new XWPFTableRow(Clone<CT_Row>(ti.RowTemp), table);
                    table.AddRow(nRow);
                    ReplaceRow(nRow, (Dictionary<string, object>)ti.Data[i]);
                }

            }
            foreach (var row in table.Rows)
            {
                foreach (var cell in row.GetTableCells())
                {
                    foreach (var para in cell.Paragraphs)
                    {
                        ReplaceKey(para, data);
                    }
                }
            }
        }
        //写文件
        FileStream outFile = new FileStream(outPath, FileMode.Create);
        doc.Write(outFile);
        outFile.Close();
    }
}
```

模板文件要设置为“始终复制”  

![IMG](https://gitee.com/imgrep001/m1/raw/master/2020/11/21/20201121185700.png)



### 示例源码： 

```
https://github.com/raikay/Demo/tree/master/NpoiDemo/NpoiWordDemo
```

鸣谢：
```
https://www.cnblogs.com/jomzhang/p/9555397.html
https://www.cnblogs.com/deeround/p/11478610.html
https://github.com/YSGStudyHards/NPOI-ExportWordAndExcel-ImportExcelData
```