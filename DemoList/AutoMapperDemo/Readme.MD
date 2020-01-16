
### AutoMapper:实体间的映射(数据库Model转Dto)

### 1、创建web项目
### 2、创建Model
```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMapperDemo
{
    public class Entity
    {
    }

    public class PostModel
    {
        public Guid Id { get; set; }
        public long SerialNo { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public short CategoryCode { get; set; }
        public bool IsDraft { get; set; }
        public string Content { get; set; }
        public DateTime ReleaseDate { get; set; }
        public virtual IList<CommentModel> Comments { get; set; }
    }
    public class CommentModel
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Content { get; set; }

        public DateTime CommentDate { get; set; }
    }
}

```
### 3、创建Dto

```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMapperDemo
{
    public class ViewModel
    {
    }
    public class PostViewModel
    {
        public Guid Id { get; set; }
        public long SerialNo { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public short CategoryCode { get; set; }
        public string Category => CategoryCode == 1001 ? ".NET" : "杂谈";
        public string ReleaseDate { get; set; }
        public short CommentCounts { get; set; }
        public virtual int Count { get; set; }
    }
}

```


### 4、创建映射规则
```
using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMapperDemo
{
    public class PostProfile : Profile
    {
        /// <summary>
        /// ctor
        /// </summary>
        public PostProfile()
        {
            // 配置 mapping 规则
            //
            CreateMap<PostModel, PostViewModel>()
                .ForMember(destination => destination.CommentCounts, source => source.MapFrom(i => i.Comments.Count()))
                .ForMember(destination => destination.ReleaseDate, source => source.ConvertUsing(new DateTimeConverter()));
        }
    }

    public class DateTimeConverter : IValueConverter<DateTime, string>
    {
        public string Convert(DateTime source, ResolutionContext context)
            => source.ToString("yyyy-MM-dd HH:mm:ss");
    }
}

```

### 5、创建mapper扩展
映射规则注入
```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace AutoMapperDemo
{

    /// <summary>
    /// Automapper configuration extension method
    /// </summary>
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
        {
            // 从appsettings.json获取映射程序程序集信息
            string assemblies = "AutoMapperDemo";

            if (!string.IsNullOrEmpty(assemblies))
            {
                var profiles = new List<Type>();

                // 映射文件类 类型
                var parentType = typeof(Profile);

                foreach (var item in assemblies.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    // 获取继承配置文件类的所有类
                    var types = Assembly.Load(item).GetTypes()
                        .Where(i => i.BaseType != null && i.BaseType.Name == parentType.Name);

                    if (types.Count() != 0 || types.Any())
                        profiles.AddRange(types);
                }

                // 添加映射规则
                if (profiles.Count() != 0 || profiles.Any())
                    services.AddAutoMapper(profiles.ToArray());
            }

            return services;
        }
    }
}

```


### 6 Startup.ConfigureServices中注册
```
services.AddAutoMapperProfiles();
```

### 7 查看映射
```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AutoMapperDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMapper _mapper;
        public WeatherForecastController(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IList<PostViewModel>> Get(PostViewModel request)
        {

            IList<CommentModel> comList = new List<CommentModel>
            {
                new CommentModel
                {
                    CommentDate=DateTime.Now,
                    Content="内容",
                    Email="sdfas@dfs.cd",
                    Id=Guid.NewGuid()
                }
            };

            List<PostModel> datas = new List<PostModel>
            {
                new PostModel
                {
                    Author="作者1",
                    CategoryCode=11,
                    Comments=comList,
                    Content="Content1",
                    Id=Guid.NewGuid(),
                    Image="img1",
                    IsDraft=true,
                    ReleaseDate=DateTime.Now,
                    SerialNo=23445387,
                    Title="title1"
                }
            };
            var model = _mapper.Map<IList<PostModel>, IList<PostViewModel>>(datas);
            return model;
        }
    }
}

```

