# IdentityServer4 单点登录Demo SingleSignOn



四种授权模式

Implicit：简化模式；直接通过浏览器的链接跳转申请令牌。

Client Credentials：客户端凭证模式；服务接口授权用这种，需要客户端id和key就可以拿到授权，访问服务端api时用，后端调用被保护的api

```
（A）客户端向认证服务器进行身份认证，并要求一个访问令牌。
（B）认证服务器确认无误后，向客户端提供访问令牌。
```



Resource Owner Password Credentials：密码模式，在客户端凭证模式基础上加用户名密码

```
（A）用户向客户端提供用户名和密码。
（B）客户端将用户名和密码发给认证服务器，向后者请求令牌。
（C）认证服务器确认无误后，向客户端提供访问令牌。
```



Authorization Code：授权码模式；类似QQ 微信等授权登录。

```
授权码模式详细流程：
（A）用户访问客户端，后者将前者导向认证服务器。
（B）用户选择是否给予客户端授权。
（C）假设用户给予授权，认证服务器将用户导向客户端事先指定的"重定向URI"（redirection URI），同时附上一个授权码。
（D）客户端收到授权码，附上早先的"重定向URI"，向认证服务器申请令牌。这一步是在客户端的后台的服务器上完成的，对用户不可见。
（E）认证服务器核对了授权码和重定向URI，确认无误后，向客户端发送访问令牌（access token）和更新令牌（refresh token）。
```





三个项目都需要添加IdentityServer4引用。  

![](https://imgkr.cn-bj.ufileos.com/6bac4c09-9eb0-423e-a781-db873e5db254.png)



Server端 `app.UseIdentityServer();`必须添加在路由和MVC前  

Client端 `app.UseAuthentication();` 必须添加在路由和MVC前  

登录成功 写Cookie
```
 Microsoft.AspNetCore.Authentication.AuthenticationProperties props = 
                new Microsoft.AspNetCore.Authentication.AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(1))
                };
                await HttpContext.SignInAsync(user.Id.ToString(), user.UserName, props);
```


在解决方案右键，三个项目同时启动。  

![](https://imgkr.cn-bj.ufileos.com/53d329eb-0ba3-4bbd-aed8-7c5024de8d42.png)

如果是.Net Core 2.1升级至3.1，startup.cs中配置有写区别。会导致升级后写入cookie失败。需要注意一下。  
该本是.Net Core 3.1      

调试 SSO.ClientC  

![](https://imgkr.cn-bj.ufileos.com/16c9f6b2-aea5-49f0-b31d-b902b57171ba.png)



参考：
https://www.cnblogs.com/lxb218/p/9419185.html  

--

---

参考

https://www.jianshu.com/p/56b577d8f786







```\
new Client
{
    ClientId = "console client",
    ClientName = "Client Credentials Client",

    AllowedGrantTypes =GrantTypes.ResourceOwnerPassword, //GrantTypes.ClientCredentials,
    AllowOfflineAccess = true,//允许刷新token
    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },
    AccessTokenLifetime=60*10,//token有效期
    //AbsoluteRefreshTokenLifetime
    AccessTokenType=AccessTokenType.Jwt,//AccessTokenType.Reference,
    AllowedScopes = { "api1",
        IdentityServerConstants.StandardScopes.OpenId,
        IdentityServerConstants.StandardScopes.Email,
        IdentityServerConstants.StandardScopes.Address,
        IdentityServerConstants.StandardScopes.Phone,
        IdentityServerConstants.StandardScopes.Profile
    }
},
```

名字ClientName随意，ClientId必须保持一致





获取userinfo

```
http://localhost:5800/connect/userinfo
```



获取token:

```
http://localhost:5800/connect/token
```

form-data:

```
grant_type:password
client_id:Auth
client_secret:Auth
username:admin
password:1234
scope:address Auth email offline_access openid phone profile
//:client_credentials
//refresh_token:hifEmkoixfdt3xbXCZwt7gs1qjDChSQDEumkA21TbK4
```



client_credentials

 理解OAuth 2.0：http://www.ruanyifeng.com/blog/2014/05/oauth_2_0.html