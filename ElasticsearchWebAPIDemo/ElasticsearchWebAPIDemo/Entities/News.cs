using Nest;

namespace ElasticsearchWebAPIDemo.Entities
{
    [ElasticsearchType(IdProperty = "Id")]
    public class News
    {
        /// <summary>
        /// 
        /// </summary>
        [Keyword]
        public long Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Keyword]
        public string Title { get; set; }

        [Keyword]
        public string Auther { get; set; }

        [Keyword]
        public string CreateTime { get; set; }


        [Keyword]
        public string Content { get; set; }
    }
}
