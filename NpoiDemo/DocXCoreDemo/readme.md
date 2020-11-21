

## DocXCore 根据模板导出 word文档

### 文档模板：

![模板图片](https://gitee.com/imgrep001/m1/raw/master/2020/11/21/20201121183654.png)

单一字段替换：\#batchNo#  

需要循环的表格：$mx.NTime$  

### 生成后文档：

![IMG](https://gitee.com/imgrep001/m1/raw/master/2020/11/21/20201121183828.png)



数据字典格式

```c#
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



鸣谢：

```
https://archive.codeplex.com/?p=docx
https://github.com/xceedsoftware/docx
```

