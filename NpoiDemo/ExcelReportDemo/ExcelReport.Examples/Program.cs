//using ExcelReport.Driver.NPOI;
//using ExcelReport.Renderers;
//using System;
//using System.Drawing;
//using ExcelReport.Driver.CSV;

using ExcelReport.Driver.CSV;
using ExcelReport.Driver.NPOI;
using ExcelReport.Renderers;
using System;
using System.Drawing;

namespace ExcelReport.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Param();
            SingleLine();
            MoreLines();
            CSVTest();
            Console.ReadKey();

        }
        
        /// <summary>
        /// 参数渲染器示例
        /// </summary>
        private static void Param()
        {
            try
            {
                // 项目启动时，添加
                Configurator.Put(".xls", new WorkbookLoader());

                ExportHelper.ExportToLocal(@"Template\01.Template.xls", "Output-01.Templat.xls",
                        new SheetRenderer("参数渲染示例",
                            new ParameterRenderer("String", "Hello World!"),
                            new ParameterRenderer("Boolean", true),
                            new ParameterRenderer("DateTime", DateTime.Now),
                            new ParameterRenderer("Double", 3.14),
                            new ParameterRenderer("Image", Image.FromFile("Image/C#高级编程.jpg"))
                            )
                        );
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("参数渲染器示例 finished!");
        }

        /// <summary>
        /// 2.单行重复渲染示例
        /// </summary>
        private static void SingleLine()
        {
            // 项目启动时，添加
            Configurator.Put(".xls", new WorkbookLoader());

            try
            {
                var num = 1;
                ExportHelper.ExportToLocal(@"Template\02.Template.xls", "Output-02.Template.xls",
                        new SheetRenderer("学生名册",
                            new RepeaterRenderer<StudentInfo>("Roster", StudentLogic.GetList(),
                                new ParameterRenderer<StudentInfo>("No", t => num++),
                                new ParameterRenderer<StudentInfo>("Name", t => t.Name),
                                new ParameterRenderer<StudentInfo>("Gender", t => t.Gender ? "男" : "女"),
                                new ParameterRenderer<StudentInfo>("Class", t => t.Class),
                                new ParameterRenderer<StudentInfo>("RecordNo", t => t.RecordNo),
                                new ParameterRenderer<StudentInfo>("Phone", t => t.Phone),
                                new ParameterRenderer<StudentInfo>("Email", t => t.Email)
                                ),
                             new ParameterRenderer("Author", "hzx")
                            )
                        );
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("单行重复渲染示例 finished!");
        }


        /// <summary>
        /// 3.多行重复渲染示例
        /// </summary>
        private static void MoreLines()
        {
            // 项目启动时，添加
            Configurator.Put(".xls", new WorkbookLoader());

            try
            {
                ExportHelper.ExportToLocal(@"Template\03.Template.xls", "Output-03.Template.xls",
                    new SheetRenderer("多行重复渲染示例",
                        new RepeaterRenderer<StudentInfo>("rptStudentInfo", StudentLogic.GetList(),
                            new ParameterRenderer<StudentInfo>("Name", t => t.Name),
                            new ParameterRenderer<StudentInfo>("Gender", t => t.Gender ? "男" : "女"),
                            new ParameterRenderer<StudentInfo>("Class", t => t.Class),
                            new ParameterRenderer<StudentInfo>("RecordNo", t => t.RecordNo),
                            new ParameterRenderer<StudentInfo>("Phone", t => t.Phone),
                            new ParameterRenderer<StudentInfo>("Email", t => t.Email)
                            )
                        )
                    );
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("多行重复渲染示例 finished!");
        }

        
        /// <summary>
        /// 4.CSV示例
        /// </summary>
        private static void CSVTest()
        {
            // 项目启动时，添加
            Configurator.Put(".csv", new CsvWorkbookLoader());

            try
            {
                var num = 1;
                ExportHelper.ExportToLocal(@"Template\04.CsvTempate.csv", "Output-04.CsvTempate.csv",
                        new SheetRenderer("04.CsvTempate",
                            new RepeaterRenderer<StudentInfo>("Roster", StudentLogic.GetList(),
                                new ParameterRenderer<StudentInfo>("No", t => num++),
                                new ParameterRenderer<StudentInfo>("Name", t => t.Name),
                                new ParameterRenderer<StudentInfo>("Gender", t => t.Gender ? "男" : "女"),
                                new ParameterRenderer<StudentInfo>("Class", t => t.Class),
                                new ParameterRenderer<StudentInfo>("RecordNo", t => t.RecordNo),
                                new ParameterRenderer<StudentInfo>("Phone", t => t.Phone),
                                new ParameterRenderer<StudentInfo>("Email", t => t.Email)
                                ),
                             new ParameterRenderer("Author", "hzx")
                            )
                        );
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("CSV示例 finished!");
            Console.ReadKey();
        }

    }
}
