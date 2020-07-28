using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientFactoryDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        OrderServiceClient _orderServiceClient;
        public OrderController(OrderServiceClient orderServiceClient)
        {
            _orderServiceClient = orderServiceClient;
        }

        [HttpGet("Get")]
        public async Task<string> Get()
        {
            return await _orderServiceClient.Get();
        }

        [HttpGet("NamedGet")]
        public async Task<string> NamedGet([FromServices] NamedOrderServiceClient serviceClient)
        {
            return await serviceClient.Get();
        }

        [HttpGet("TypedGet")]
        public async Task<string> TypedGet([FromServices] TypedOrderServiceClient serviceClient)
        {
            return await serviceClient.Get();
        }
    }

    //服务

#region NamedOrderServiceClient.cs
public class NamedOrderServiceClient
{
    IHttpClientFactory _httpClientFactory;

    const string _clientName = "NamedOrderServiceClient";  //定义客户端名称

    public NamedOrderServiceClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }


    public async Task<string> Get()
    {
        var client = _httpClientFactory.CreateClient(_clientName); //使用客户端名称获取客户端

        //使用client发起HTTP请求,这里使用相对路径来访问
        return await client.GetStringAsync("/OrderService");
    }
}
#endregion

    #region OrderServiceClient.cs
    public class OrderServiceClient
    {
        IHttpClientFactory _httpClientFactory;

        public OrderServiceClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<string> Get()
        {
            var client = _httpClientFactory.CreateClient();

            //使用client发起HTTP请求
            var val = await client.GetStringAsync("http://localhost:5003/OrderService");
            return val;
        }
    }
    #endregion

    #region TypedOrderServiceClient.cs
    public class TypedOrderServiceClient
    {
        HttpClient _client;
        public TypedOrderServiceClient(HttpClient client)
        {
            _client = client;
        }


        public async Task<string> Get()
        {
            return await _client.GetStringAsync("/OrderService"); //这里使用相对路径来访问
        }
    }
    #endregion




    //管道
    #region MyRegion
    public class RequestIdDelegatingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            //处理请求
            request.Headers.Add("x-guid", Guid.NewGuid().ToString());

            var result = await base.SendAsync(request, cancellationToken); //调用内部handler

            //处理响应

            return result;
        }
    }
    #endregion
}
