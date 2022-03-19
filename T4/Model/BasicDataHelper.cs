using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Model
{
    public class BasicDataDto
    {
        /// <summary>
        /// 注释
        /// </summary>
        public string Annotation { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string TabName { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string PropertyDataType { get; set; }

        /// <summary>
        /// 最大长度
        /// </summary>
        public int MaxLength { get; set; }
    }

    public class MySqlBasicDataHelper
    {
        private static string connString = "server=47.94.158.147;userid=root;password=password!@#$;database=testripx;";

        public static List<T> Query<T>(string DbName)
        {
            string SqlStr = $@"SELECT TABLE_NAME TabName,COLUMN_NAME PropertyName,COLUMN_COMMENT Annotation,DATA_TYPE PropertyDataType,CHARACTER_MAXIMUM_LENGTH MaxLength FROM information_schema.COLUMNS WHERE TABLE_SCHEMA ='{DbName}'";
            var TLst = new List<T>();
            using (var sqlConnection = new MySqlConnection(connString))
            {
                sqlConnection.Open();
                IDbConnection conn = sqlConnection;
                TLst = conn.Query<T>(SqlStr).ToList();
            }
            return TLst;
        }

        public static string GetType(string PropertyType, int MaxLength)
        {
            string Result = string.Empty;
            switch (PropertyType)
            {

                case "bit":
                    Result = "bool";
                    break;
                case "time":
                    Result = "TimeSpan";
                    break;
                case "datetime":
                    Result = "DateTime";
                    break;
                case "decimal":
                    Result = "decimal";
                    break;
                case "float":
                    Result = "float";
                    break;
                case "int":
                case "tinyint":
                case "smallint":
                    Result = "int";
                    break;
                case "char":
                    if (MaxLength == 36)
                        Result = "Guid";
                    else
                        Result = "string";
                    break;
                default:
                    Result = "string";
                    break;
            }
            return Result;
        }
    }
}
