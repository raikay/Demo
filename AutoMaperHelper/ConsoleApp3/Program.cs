using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            MapToTest();
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
        #region model

        public class UserInfo
        {
            public int PK { get; set; }
            public int Age { get; set; }

            /// <summary>
            /// 用户ID
            /// </summary>
            public string UserId { get; set; }
            /// <summary>
            /// 真实姓名
            /// </summary>
            public string RealName { get; set; }
            /// <summary>
            /// 部门ID
            /// </summary>
            public string DepartmentId { get; set; }
            /// <summary>
            /// 部门名称
            /// </summary>
            public string DepartmentName { get; set; }
            /// <summary>
            /// 是否在线 1-在线 0-离线
            /// </summary>
            public int UserOnLine { get; set; }

            public DateTime CreateTime { get; set; }

            public int Sex { get; set; }
        }

        public class UserInfoDTO
        {
            //public int UID { get; set; }
            //public int Age { get; set; }
            //public string Ex { get; set; }
            //public DateTime AddTime { get; set; }

            /// <summary>
            /// 用户ID
            /// </summary>
            public string UserId { get; set; }
            /// <summary>
            /// 真实姓名
            /// </summary>
            public string RealName { get; set; }
            /// <summary>
            /// 部门ID
            /// </summary>
            public string DepartmentId { get; set; }
            /// <summary>
            /// 部门名称
            /// </summary>
            public string DepartmentName { get; set; }
            /// <summary>
            /// 是否在线 1-在线 0-离线
            /// </summary>
            public int UserOnLine { get; set; }
        }
        #endregion

        private static void MapToTest()
        {
            UserInfo userInfo = new UserInfo
            {
                PK = 1,
                UserId = "2018001",
                RealName = "大师兄",
                DepartmentId = "001",
                DepartmentName = "技术部",
                Sex = 1,
                UserOnLine = 1,
                CreateTime = DateTime.Now,
                Age = 10
            };

            IEnumerable<UserInfo> userInfos = new List<UserInfo>
            {
                new UserInfo
                {
                    PK = 1,
                    UserId = "2018001",
                    RealName = "MrZhaoYi",
                    DepartmentId = "001",
                    DepartmentName = "技术部",
                    Sex = 1,
                    UserOnLine = 1,
                    CreateTime = DateTime.Now
                },
                new UserInfo
                {
                    PK = 2,
                    UserId = "2018002",
                    RealName = "MrZhaoYi2",
                    DepartmentId = "002",
                    DepartmentName = "技术部",
                    Sex = 1,
                    UserOnLine = 1,
                    CreateTime = DateTime.Now
                }
            };

            DataTable dataTable = new DataTable("MyTable");
            DataColumn column = new DataColumn("UserId", typeof(string));
            dataTable.Columns.Add(column);

            DataRow dr = dataTable.NewRow();
            dr["UserId"] = "003";
            dataTable.Rows.Add(dr);

            //UserInfoDTO dto1 = userInfo.MapTo<UserInfoDTO>();

            UserInfoDTO dto2 = userInfo.MapTo<UserInfo, UserInfoDTO>();

            List<UserInfoDTO> userInfoDtos1 = userInfos.MapTo<UserInfo, UserInfoDTO>();

            //List<UserInfoDTO> userInfoDtos2 = userInfos.MapTo<UserInfoDTO>();

            List<UserInfoDTO> userInfoDtos3 = dataTable.MapTo<UserInfoDTO>();

            //viewmodel与实体字段名字没有全部对应，只有几个字段的名字和源实体中的字段名字是一样的，其他的字段是通过实体中的几个字段组合或者是格式或者是类型转化而来的
            //var config2 = new MapperConfiguration(
            //    cfg => cfg.CreateMap<UserInfo, UserInfoDTO>()
            //        .ForMember(d => d.UID, opt => opt.MapFrom(s => s.PK))  //指定字段一一对应
            //        .ForMember(d => d.AddTime, opt => opt.MapFrom(src => src.CreateTime.ToString("yy-MM-dd")))//指定字段，并转化指定的格式
            //        .ForMember(d => d.Age, opt => opt.Condition(src => src.Age > 5))//条件赋值
            //        .ForMember(d => d.DepartmentName, opt => opt.Ignore())//忽略该字段，不给该字段赋值
            //        .ForMember(d => d.RealName, opt => opt.NullSubstitute("Default Value"))//如果源字段值为空，则赋值为 Default Value
            //        .ForMember(d => d.Ex, opt => opt.MapFrom(src => src.PK + "_" + src.UserId + "_" + src.RealName)));//可以自己随意组合赋值
            //var mapper2 = config2.CreateMapper();
            //UserInfoDTO dto1 = mapper2.Map<UserInfoDTO>(userInfo);

            //Console.WriteLine(dto1.RealName);
        }
    }


    /// <summary>
    /// AutoMapper扩展帮助类
    /// </summary>
    public static class AutoMapperExtension
    {
        /// <summary>
        /// 类型映射
        /// </summary>
        /// <typeparam name="TDestination">映射后的对象</typeparam>
        /// <param name="obj">要映射的对象</param>
        /// <returns></returns>
        public static TDestination MapTo<TDestination>(this object obj) where TDestination : class
        {
            if (obj == null) return default(TDestination);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDestination, object>());
            var mapper = config.CreateMapper();
            return mapper.Map<TDestination>(obj);
        }
        /// <summary>
        ///  单个对象映射
        /// </summary>
        public static T MapToNew<T>(this object obj)
        {
            if (obj == null) return default(T);
            CreateMap(obj.GetType(), typeof(T));
            return Mapper.Map<T>(obj);
        }
        /// <summary>
        /// 集合列表类型映射
        /// </summary>
        /// <typeparam name="TDestination">目标对象类型</typeparam>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static List<TDestination> MapTo<TDestination>(this IEnumerable source) where TDestination : class
        {
            if (source == null) return default(List<TDestination>);

            var config = new MapperConfiguration(cfg => cfg.CreateMap(source.GetType(), typeof(TDestination)));
            var mapper = config.CreateMapper();
            return mapper.Map<List<TDestination>>(source);
        }

        /// <summary>
        /// 集合列表类型映射
        /// </summary>
        /// <typeparam name="TSource">数据源类型</typeparam>
        /// <typeparam name="TDestination">目标对象类型</typeparam>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static List<TDestination> MapTo<TSource, TDestination>(this IEnumerable<TSource> source)
            where TDestination : class
            where TSource : class
        {
            if (source == null) return new List<TDestination>();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper = config.CreateMapper();
            return mapper.Map<List<TDestination>>(source);
        }

        /// <summary>
        /// 集合列表类型映射
        /// </summary>
        /// <typeparam name="TSource">数据源类型</typeparam>
        /// <typeparam name="TDestination">目标对象类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="configure">自定义配置</param>
        /// <returns></returns>
        public static List<TDestination> MapTo<TSource, TDestination>(this IEnumerable<TSource> source, Action<IMapperConfigurationExpression> configure)
            where TDestination : class
            where TSource : class
        {
            if (source == null) return new List<TDestination>();

            var config = new MapperConfiguration(configure);
            var mapper = config.CreateMapper();
            return mapper.Map<List<TDestination>>(source);
        }

        /// <summary>
        /// 类型映射
        /// </summary>
        /// <typeparam name="TSource">数据源类型</typeparam>
        /// <typeparam name="TDestination">目标对象类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="destination">目标对象</param>
        /// <returns></returns>
        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
            where TSource : class
            where TDestination : class
        {
            if (source == null) return destination;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper = config.CreateMapper();
            return mapper.Map<TSource, TDestination>(source, destination);
        }

        /// <summary>
        /// 类型映射,默认字段名字一一对应
        /// </summary>
        /// <typeparam name="TDestination">转化之后的model，可以理解为viewmodel</typeparam>
        /// <typeparam name="TSource">要被转化的实体，Entity</typeparam>
        /// <param name="source">可以使用这个扩展方法的类型，任何引用类型</param>
        /// <returns>转化之后的实体</returns>
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
            where TDestination : class
            where TSource : class
        {
            if (source == null) return default(TDestination);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDestination>());
            var mapper = config.CreateMapper();
            return mapper.Map<TDestination>(source);
        }

        /// <summary>
        /// DataReader映射
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="reader">数据源</param>
        /// <returns></returns>
        public static IEnumerable<T> MapTo<T>(this IDataReader reader)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<IDataReader, IEnumerable<T>>());
            var mapper = config.CreateMapper();
            return mapper.Map<IDataReader, IEnumerable<T>>(reader);
        }

        /// <summary>  
        /// 将 DataTable 转为实体对象  
        /// </summary>  
        /// <typeparam name="T">目标对象类型</typeparam>  
        /// <param name="dt">数据源</param>  
        /// <returns></returns>  
        public static List<T> MapTo<T>(this DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return default(List<T>);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<IDataReader, List<T>>());
            var mapper = config.CreateMapper();
            return mapper.Map<IDataReader, List<T>>(dt.CreateDataReader());
        }

        /// <summary>
        /// 将List转换为Datatable
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="list">数据源</param>
        /// <returns></returns>
        public static DataTable MapTo<T>(this IEnumerable<T> list)
        {
            if (list == null) return default(DataTable);

            //创建属性的集合
            List<PropertyInfo> pList = new List<PropertyInfo>();
            //获得反射的入口
            System.Type type = typeof(T);
            DataTable dt = new DataTable();
            //把所有的public属性加入到集合 并添加DataTable的列
            Array.ForEach<PropertyInfo>(type.GetProperties(), p => { pList.Add(p); dt.Columns.Add(p.Name, p.PropertyType); });
            foreach (var item in list)
            {
                //创建一个DataRow实例
                DataRow row = dt.NewRow();
                //给row 赋值
                pList.ForEach(p => row[p.Name] = p.GetValue(item, null));
                //加入到DataTable
                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}
