using ElasticsearchWebAPIDemo.Entities;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace ElasticsearchWebAPIDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IElasticClient _client;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, /*IESClientProvider clientProvider*/IElasticClient client)
        {
            _logger = logger;
            _client = client;
        }
        /*
        创建索引
        #put http://47.94.89.136:9200/news
         {
            "mappings": {
                "properties": {
                    "id": {
                        "type": "long"
                    },
                    "title": {
                        "type": "text",
                        "analyzer": "ik_max_word",
                        "search_analyzer": "ik_smart"
                    },
                    "content": {
                        "type": "text",
                        "analyzer": "ik_max_word",
                        "search_analyzer": "ik_smart"
                    },
                    "author": {
                        "type": "keyword"
                    },
                    "createTime": {
                        "type": "date",
                        "format": "yyyy-MM-dd HH:mm:ss||yyyy-MM-dd||epoch_millis"
                    }
                }
            }
        }



         */

        /// <summary>
        /// 创建索引
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        [HttpGet("CreateIndex")]
        public bool CreateIndex(string indexName)
        {
            var res = _client.Indices.Create(indexName, c => c.Map<News>(h => h.AutoMap().Properties(ps => ps
            .Text(s => s
                 .Name(n => n.Title)
                 .Analyzer("ik_smart")
                 .SearchAnalyzer("ik_smart")
                 )
            .Text(s => s
                 .Name(n => n.Content)
                 .Analyzer("ik_smart")
                 .SearchAnalyzer("ik_smart")
                 )
            )
            ));
            return res.IsValid;
        }

        /// <summary>
        /// 索引内插入文章
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddArticles")]
        public bool AddArticles()
        {
            // 获取数据批量进行插入
            List<News> listArticle = new List<News> { new News { Auther = "Auther1", Id = 1, Content = "subtitle1", Title = "titlle1", CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") } };

            listArticle.Add(new News { Auther = "Auther", CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Id = 2, Title = "丰县生育八孩女子事件调查组成立，中国妇女报：期待调查一锤定音", Content = @"今天，江苏省委省政府决定成立调查组，对“丰县生育八孩女子”事件进行全面调查。江苏的这一决定有助于厘清舆论焦点，回应社会关切。我们希望，尽快查明事实真相，依法严惩违法犯罪行为，对有关责任人员严肃追责，向社会及时公布调查结果，还受害人一个公道，给公众一个一锤定音的结论。同时，我们相信，在各方的共同努力下，受害的妇女能得到精心的治疗，孩子们都能得到温暖的照护，健康平安成长。" });
            listArticle.Add(new News { Auther = "Auther", CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Id = 3, Title = "文科生创“冰墩墩”字体火了，美术生毫不示弱，用大饼画出冰墩墩", Content = "要说中国最近这段时间最火的事情，那莫过于就是冬奥运会了，冬奥运会是属于冬季的运动赛事，冬奥会，是世界上规模最大的冬季综合性运动会，在全球上所有国家里每四年举办一届，这种盛大的运动会项目如今在我国内北京、河北张家口联合举行。" });
            listArticle.Add(new News { Auther = "Auther", CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Id = 4, Title = "马云套现430亿宣告退休，若支付宝破产，余额宝的钱怎么处理", Content = @"当看到撒贝宁活跃于各大综艺，而且完全打开自己的时候，相信大家第一时间肯定会想到马云，因为很多人都说是马云开启了撒贝宁的世界大门。马云在我国的知名度很高，他曾经在撒贝宁主持的一档节目当中说，自己对钱没有感兴趣，后来撒贝宁就开始走上了谐星之路。" });
            listArticle.Add(new News { Auther = "Auther", CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Id = 5, Title = "丰县铁链女的事终于有了详细的调查结果", Content = "丰县铁链女的事终于有了详细的调查结果，我相信江苏省的通报取信了互联网上的大多数人。然而我想说，这不是胜利，铁链女作为一个基层事件，县里说不清楚，徐州市说不清楚，最后要三级跳到江苏省委省政府组织调查组，才做到把充斥互联网的质疑大体驱散，这当中只有教训，只有要认真改变基层工作作风、下力气修补官方公信力的紧迫性。" });

            //for (int i = 3; i < 100; i++)
            //{
            //    listArticle.Add(new Article { Auther = "Auther"+i, Id = Guid.NewGuid().ToString(), SubTitle = "subtitle"+i, Title = "titlle"+i });
            //}

            //var ss = _client.IndexMany(listArticle);
            var article =  new News { Auther = "Auther1", Id = 1, Content = "subtitle1", Title = "titlle1", CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
            //插入一条，默认索引
            var s1 = _client.IndexDocument(article);
            //插入一条，指定索引
            var s2 = _client.Index(article, s => s.Index("news"));
            return _client.IndexMany(listArticle).IsValid;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }



        [HttpPost]
        [Route("SearchHighlight")]
        public List<News> SearchHighlight(string key, int pageIndex = 0, int pageSize = 10)
        {
            //所有字段
            var searchAll = _client.Search<News>(s => s
                .From(pageIndex)
                .Size(pageSize)
                .Query(q => q
                    .QueryString(qs => qs
                    .Query(key)
                    .DefaultOperator(Operator.Or)))
                );

            ////一个字段
            //var searchAll = _client.Search<News>(a => a
            //.Query(a =>
            //    a.Match(m =>
            //        m.Field(f => f.Content).Query(key))));

            return searchAll.Documents.ToList();
        }
    }
}