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
        ��������
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
        /// ��������
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
        /// �����ڲ�������
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddArticles")]
        public bool AddArticles()
        {
            // ��ȡ�����������в���
            List<News> listArticle = new List<News> { new News { Auther = "Auther1", Id = 1, Content = "subtitle1", Title = "titlle1", CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") } };

            listArticle.Add(new News { Auther = "Auther", CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Id = 2, Title = "���������˺�Ů���¼�������������й���Ů�����ڴ�����һ������", Content = @"���죬����ʡίʡ�����������������飬�ԡ����������˺�Ů�ӡ��¼�����ȫ����顣���յ���һ�����������������۽��㣬��Ӧ�����С�����ϣ�������������ʵ���࣬�����ϳ�Υ��������Ϊ�����й�������Ա����׷������ἰʱ���������������ܺ���һ��������������һ��һ�������Ľ��ۡ�ͬʱ���������ţ��ڸ����Ĺ�ͬŬ���£��ܺ��ĸ�Ů�ܵõ����ĵ����ƣ������Ƕ��ܵõ���ů���ջ�������ƽ���ɳ���" });
            listArticle.Add(new News { Auther = "Auther", CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Id = 3, Title = "�Ŀ����������նա�������ˣ�����������ʾ�����ô���������ն�", Content = "Ҫ˵�й�������ʱ���������飬��Ī���ھ��Ƕ����˻��ˣ������˻������ڶ������˶����£����»ᣬ�������Ϲ�ģ���Ķ����ۺ����˶��ᣬ��ȫ�������й�����ÿ����ٰ�һ�죬����ʢ����˶�����Ŀ������ҹ��ڱ������ӱ��żҿ����Ͼ��С�" });
            listArticle.Add(new News { Auther = "Auther", CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Id = 4, Title = "��������430���������ݣ���֧�����Ʋ�������Ǯ��ô����", Content = @"��������������Ծ�ڸ������գ�������ȫ���Լ���ʱ�����Ŵ�ҵ�һʱ��϶����뵽���ƣ���Ϊ�ܶ��˶�˵�����ƿ�������������������š��������ҹ���֪���Ⱥܸߣ������������������ֵ�һ����Ŀ����˵���Լ���Ǯû�и���Ȥ�������������Ϳ�ʼ������г��֮·��" });
            listArticle.Add(new News { Auther = "Auther", CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Id = 5, Title = "��������Ů��������������ϸ�ĵ�����", Content = "��������Ů��������������ϸ�ĵ������������Ž���ʡ��ͨ��ȡ���˻������ϵĴ�����ˡ�Ȼ������˵���ⲻ��ʤ��������Ů��Ϊһ�������¼�������˵�������������˵����������Ҫ������������ʡίʡ������֯�����飬�������ѳ�⻥���������ɴ�����ɢ���⵱��ֻ�н�ѵ��ֻ��Ҫ����ı���㹤�����硢�������޲��ٷ��������Ľ����ԡ�" });

            //for (int i = 3; i < 100; i++)
            //{
            //    listArticle.Add(new Article { Auther = "Auther"+i, Id = Guid.NewGuid().ToString(), SubTitle = "subtitle"+i, Title = "titlle"+i });
            //}

            //var ss = _client.IndexMany(listArticle);
            var article =  new News { Auther = "Auther1", Id = 1, Content = "subtitle1", Title = "titlle1", CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
            //����һ����Ĭ������
            var s1 = _client.IndexDocument(article);
            //����һ����ָ������
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
            //�����ֶ�
            var searchAll = _client.Search<News>(s => s
                .From(pageIndex)
                .Size(pageSize)
                .Query(q => q
                    .QueryString(qs => qs
                    .Query(key)
                    .DefaultOperator(Operator.Or)))
                );

            ////һ���ֶ�
            //var searchAll = _client.Search<News>(a => a
            //.Query(a =>
            //    a.Match(m =>
            //        m.Field(f => f.Content).Query(key))));

            return searchAll.Documents.ToList();
        }
    }
}