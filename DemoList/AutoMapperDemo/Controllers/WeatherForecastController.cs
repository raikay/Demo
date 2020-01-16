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
