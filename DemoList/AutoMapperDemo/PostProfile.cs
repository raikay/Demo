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
