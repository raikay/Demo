﻿using System;
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
        public async Task<IList<PostViewModel>> Get()
        {
            //低版本
            //Mapper.Initialize(x => x.CreateMap<Destination, Source>());
            //Source source = AutoMapper.Mapper.Map<Source>(des);
            //Console.WriteLine(source.InfoUrl);

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
                    Author=null,
                    CategoryCode=1001,
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

            IList<PostViewModel> list = new List<PostViewModel> {new PostViewModel
            {
                Id=Guid.NewGuid(),
                SerialNo=111111
            } };
            list = _mapper.Map<IList<PostModel>, IList<PostViewModel>>(datas);
            return list;
        }
    }
}