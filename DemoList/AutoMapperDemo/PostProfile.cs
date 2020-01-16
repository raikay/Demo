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
                .ForMember(t => t.CommentCounts, s => s.MapFrom(i => i.Comments.Count()))
                .ForMember(t => t.AuthorName, s => s.MapFrom(i => i.Author.Substring(1)))
                .ForMember(t => t.Author, s => s.Ignore())
                .ForMember(t => t.Id, s => s.Ignore())
                .ForMember(t => t.SerialNo, s => s.Ignore())
                .ForMember(t => t.SubDate, s => s.MapFrom(i => i.ReleaseDate.ToString("yyyy年MM月dd日")))
                .ForMember(t => t.ReleaseDate, s => s.ConvertUsing(new DateTimeConverter()));

        }
    }

    public class DateTimeConverter : IValueConverter<DateTime, string>
    {
        public string Convert(DateTime source, ResolutionContext context)
            => source.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
