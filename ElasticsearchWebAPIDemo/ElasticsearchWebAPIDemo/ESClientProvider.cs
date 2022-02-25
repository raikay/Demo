using Nest;

namespace ElasticsearchWebAPIDemo
{
    class ESClientProvider : IESClientProvider
    {
        private ElasticClient? _client;


        public ElasticClient GetClient()
        {
            if (_client != null)
                return _client;

            InitClient();
            return _client;
        }
        private void InitClient()
        {
            ////集群创建
            //var nodes = new[] { new Uri("http://localhost:9200") };
            //var pool = new Elasticsearch.Net.StaticConnectionPool(nodes);
            //var settings = new ConnectionSettings(pool); ;
            //var client = new ElasticClient(settings);


            var node = new Uri("http://47.94.89.136:9200");
            _client = new ElasticClient(new ConnectionSettings(node).DefaultIndex("news"));
        }
    }
    public interface IESClientProvider
    {
        ElasticClient GetClient();
    }


    /*
     
    var nodes = new[]
            {
    new Uri("http://localhost:9200")
};

var pool = new StaticConnectionPool(nodes);
var settings = new ConnectionSettings(pool); ;
var client = new ElasticClient(settings);
     */



    public static class ElasticsearchExtensions
    {
        public static void AddElasticsearch(this IServiceCollection services)
        {
            var url = "http://47.94.89.136:9200";
            var defaultIndex = "news";

            var settings = new ConnectionSettings(new Uri(url)).DefaultIndex(defaultIndex).EnableDebugMode();



            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);
        }
    }

}
