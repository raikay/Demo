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
            var node = new Uri("http://47.94.89.136:9200");
            _client = new ElasticClient(new ConnectionSettings(node).DefaultIndex("news"));
        }
    }
    public interface IESClientProvider
    {
        ElasticClient GetClient();
    }
}
