using System;
using System.Net.Http;
using System.Threading.Tasks;
using AspNetCoregRpcService;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;

namespace AspNetCoreGrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //AppContext.SetSwitch(
            //    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
    //        var httpClientHandler = new HttpClientHandler();
    //        // Return `true` to allow certificates that are untrusted/invalid 
    //        httpClientHandler.ServerCertificateCustomValidationCallback =
    //HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
    //        var httpClient = new HttpClient(httpClientHandler);



            //var channel = GrpcChannel.ForAddress("https://game.raikay.com/");
            var channel = GrpcChannel.ForAddress("https://game.raikay.com/");//, new GrpcChannelOptions { HttpClient = httpClient }
            //var channel = GrpcChannel.ForAddress("http://us-or-cera-1.natfrp.cloud:33000/", new GrpcChannelOptions { HttpClient = httpClient });//
            // var channel = GrpcChannel.ForAddress("http://game.raikay.com:33000/");

            //LuCat.LuCatClient()  添加LuCat.proto文件后自动生成，和服务端LuCat.proto文件一致
            var catClient = new LuCat.LuCatClient(channel);
            var catReply = await catClient.SuckingCatAsync(new ParamRequest { Id=0});
            Console.WriteLine(""+ catReply.Message);
            Console.ReadKey();
        }
    }
}
