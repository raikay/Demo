using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMapperDemo
{
    #region Initialize

    public class PostAppService : IPostAppService
    {

        /// <summary>
        ///
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="post"></param>
        /// <param name="mapper"></param>
        public PostAppService(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion Initialize

        /// <summary>
        /// 获取所有的文章信息
        /// </summary>
        /// <returns></returns>
        public IList<PostViewModel> GetPostLists()
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
                },
                new PostModel
                {
                    Author="作者",
                    CategoryCode=12,
                    Comments=comList,
                    Content="Content2",
                    Id=Guid.NewGuid(),
                    Image="img2",
                    IsDraft=true,
                    ReleaseDate=DateTime.Now,
                    SerialNo=23445387,
                    Title="title2"
                }
            };
            var model = _mapper.Map<IList<PostModel>, IList<PostViewModel>>(datas);
            return model;
        }
    }

    public interface IPostAppService
    {
        #region APIs

        /// <summary>
        /// Get all post list
        /// </summary>
        /// <returns></returns>
        IList<PostViewModel> GetPostLists();

        #endregion APIs
    }
}
