using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

namespace NpoiWordDemo
{
    /// <summary>
    /// NpoiHeplper
    /// </summary>
    public class NpoiHeplper
    {
        /// <summary>
        /// 输出模板word文档
        /// </summary>
        /// <param name="tempFilePath">模板文件路径</param>
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

        private static void ReplaceRow(XWPFTableRow row, Dictionary<string, object> data)
        {
            foreach (var cell in row.GetTableCells())
            {
                foreach (var item in cell.Paragraphs)
                {
                    ReplaceParagraph(item, data, cell);
                }
            }
        }
        private static void ReplaceKey(XWPFParagraph para, Dictionary<string, object> data)
        {
            string text = "";
            foreach (var run in para.Runs)
            {
                text = run.ToString();
                foreach (var key in data.Keys)
                {
                    //$$模板中数据占位符为$KEY$
                    if (text.Contains($"#{key}#"))
                    {
                        text = text.Replace($"#{key}#", data[key]?.ToString());
                    }
                }
                run.SetText(text, 0);
            }
        }
        private static MatchCollection GetMatches(string text)
        {
            if (string.IsNullOrEmpty(text)) text = "";
            Regex regex = new Regex("[#|\\$]([a-zA-Z0-9_.]+?)[#|\\$]", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return regex.Matches(text);
        }
        private static IList<string> ResolveText(string text)
        {
            if (string.IsNullOrEmpty(text)) return new List<string>();
            text = text.Replace("\r\n", "\n").Replace("\r", "\n");
            return text.Split('\n');
        }


        private static XWPFParagraph ReplaceParagraph(XWPFParagraph p, Dictionary<string, object> data, XWPFTableCell cell)
        {
            XWPFParagraph pr = p;
            var ms = GetMatches(p.Text);
            var ks = new List<string>();
            var rs = new List<string>();
            foreach (Match m in ms)
            {
                if (m.Groups.Count > 1)
                {
                    string text = m.Groups[1].Value;
                    if (text.Contains("."))
                    {
                        var ts = text.Split(".");
                        text = ts[ts.Length - 1];
                    }
                    ks.Add(text);
                    rs.Add(m.Value);
                }
            }
            bool isCt = data.Any(op => ks.Any(o => o.Contains(op.Key)));
            bool isReplace = false;

            if (isCt)
            {
                if (ks.Count > 1)
                {
                    for (int i = 0; i < ks.Count; i++)
                    {
                        if (data.ContainsKey(ks[i]))
                        {
                            cell.SetText(data[ks[i]]?.ToString());
                            //p.
                            //p.ReplaceText(rs[i], data[ks[i]]?.ToString());
                        }
                    }
                }
                else if (ks.Count == 1)
                {
                    string text = ks[0];
                    if (data.ContainsKey(text))
                    {
                        var ct = data[text]?.ToString();
                        var cts = ResolveText(ct);
                        var pc = p;
                        foreach (var item2 in cts)
                        {
                            if (string.IsNullOrWhiteSpace(item2)) continue;
                            var pt = pc;
                            pt.ReplaceText(rs[0], item2);
                            pc = pt;
                            pr = pc;
                        }
                        isReplace = true;
                    }
                }
                if (isReplace)
                {
                    //删除原来段落
                    //p.Remove(false);
                }
            }

            return pr;
        }

        //纯辅助方法
        private class TableInfo
        {
            public bool IsList { get; set; }
            public bool IsDict { get; set; }
            public /*XWPFTableRow*/CT_Row RowTemp { get; set; }
            public IList<object> Data { get; set; }
        }

        /// <summary>
        /// 只获取列表匹配项
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static MatchCollection GetListMatches(string text)
        {
            if (string.IsNullOrEmpty(text)) text = "";
            Regex regex = new Regex("\\$([a-zA-Z0-9_.]+?)\\$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return regex.Matches(text);
        }
        //判断表格是列表还是表单
        private static TableInfo GetTableInfo(XWPFTable table, Dictionary<string, object> data)
        {
            TableInfo result = new TableInfo();
            var r0 = table.GetRow(table.Rows.Count - 1);
            var c0 = r0.GetCell(r0.GetTableCells().Count - 1);
            var ct = c0.Paragraphs[0].Text;
            var ms = GetListMatches(ct);
            foreach (Match item in ms)
            {
                if (item.Groups.Count > 1)
                {
                    string text = item.Groups[1].Value;
                    if (text.Contains("."))
                    {
                        result.IsDict = true;
                        text = text.Split('.')[0];
                    }

                    if (data.ContainsKey(text))//判断是否是列表
                    {
                        result.RowTemp = r0.GetCTRow();//new XWPFTableRow() //Clone(r0) as XWPFTableRow;
                        result.IsList = true;
                        result.Data = new List<object>();
                        var dd = data[text];
                        foreach (var item1 in data[text] as List<Dictionary<string, object>>)
                        {
                            result.Data.Add(item1);
                        }
                        break;
                    }
                }
            }
            //删除模板行
            table.RemoveRow(table.Rows.Count - 1);
            return result;
        }

        public static T Clone<T>(object obj)
        {
            object retval;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                retval = bf.Deserialize(ms);
                ms.Close();
            }
            return (T)retval;

        }

        private static void ReplaceKeyObjet(XWPFParagraph para, object model)
        {
            string text = "";
            Type t = model.GetType();
            PropertyInfo[] pi = t.GetProperties();
            foreach (var run in para.Runs)
            {
                text = run.ToString();
                foreach (PropertyInfo p in pi)
                {
                    //$$模板中数据占位符为$KEY$
                    string key = $"${p.Name}$";
                    if (text.Contains(key))
                    {
                        try
                        {
                            text = text.Replace(key, p.GetValue(model, null).ToString());
                        }
                        catch (Exception ex)
                        {
                            //可能有空指针异常
                            text = text.Replace(key, "");
                        }
                    }
                }
                run.SetText(text, 0);
            }
        }



    }
}
