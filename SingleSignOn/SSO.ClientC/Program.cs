using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static IdentityModel.OidcConstants;

namespace SSO.ClientC
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            #region 获取token
            var client = new HttpClient();
            ////获取文档、校验地址
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:50000/");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // request access token
            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = "http://localhost:50000/connect/token", //disco.TokenEndpoint,//disco.UserInfoEndpoint, //
                ClientId = "console client",
                ClientSecret = "511536EF-F270-4058-80CA-1C89C192F69A",
                Scope = "api1 offline_access",
                UserName = "admin",//如果不是密码模式,是ClientCredentials不需要 UserName 、Password，其都一样，ClientCredentials好像不能刷新token
                Password = "1234",
                GrantType = GrantTypes.Password

            });


            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            #endregion

            #region 刷新token
            //判断用户过期
            var s0 = tokenResponse.AccessToken.Split(".")[1];
            var s1 = Base64Decode(s0);

            var s2 = JsonConvert.DeserializeObject<dynamic>(s1);
            var s3 = Convert.ToString(s2.exp);
            DateTime s4 = GetDateTime(s3);
            var s5 = s4 - DateTime.Now;
            //过期时间10分钟，如果剩余过期时间小于5分钟 刷新token
            if (s5.Minutes < 5)
            {
                tokenResponse = await client.RequestRefreshTokenAsync(new RefreshTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    GrantType = GrantTypes.RefreshToken,
                    ClientId = "console client",
                    ClientSecret = "511536EF-F270-4058-80CA-1C89C192F69A",
                    Scope = "api1 offline_access",
                    RefreshToken = tokenResponse.RefreshToken

                });
            }

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }
            #endregion

            #region 请求受保护资源


            // call Identity Resource API
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            var response = await apiClient.GetAsync("http://localhost:50003/WeatherForecast");//请求受保护的API
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }

            #endregion

            Console.ReadKey();
        }

        /// <summary>
        /// 将base64格式，转换utf8
        /// </summary>
        /// <param name="content">解密内容</param>
        /// <returns></returns>
        public static string Base64Decode(string content)
        {
            byte[] bytes = Convert.FromBase64String(content + "==");//.FromBase64String(content);
            return Encoding.UTF8.GetString(bytes);
        }

        public static DateTime GetDateTime(string strLongTime)
        {
            Int64 begtime = Convert.ToInt64(strLongTime) * 10000000;//100毫微秒为单位,textBox1.text需要转化的int日期
            DateTime dt_1970 = new DateTime(1970, 1, 1, 8, 0, 0);
            long tricks_1970 = dt_1970.Ticks;//1970年1月1日刻度
            long time_tricks = tricks_1970 + begtime;//日志日期刻度
            DateTime dt = new DateTime(time_tricks);//转化为DateTim
            return dt;
        }
    }
}
