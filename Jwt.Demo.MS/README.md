

添加引用：

```c#
Microsoft.IdentityModel.Tokens
Microsoft.AspNetCore.Authentication.JwtBearer
```

常量类：

```c#
 public class Const
 {
     /// <summary>
     /// Key
     /// </summary>
     public const string SecurityKey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDI2a2EJ7m872v0afyoSDJT2o1+SitIeJSWtLJU8/Wz2m7gStexajkeD+Lka6DSTy8gt9UwfgVQo6uKjVLG5Ex7PiGOODVqAEghBuS7JzIYU5RvI543nNDAPfnJsas96mSA7L/mD7RTE2drj6hf3oZjJpMPZUQI/B1Qjb5H3K3PNwIDAQAB";
     public const string Domain = "http://localhost:5000";
 }
```



Startup.ConfigureServices:

```c#
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
                  {
                      options.TokenValidationParameters = new TokenValidationParameters
                      {
                          ValidateIssuer = true,//是否验证Issuer
                          ValidateAudience = true,//是否验证Audience
                          ValidateLifetime = true,//是否验证失效时间
                          ClockSkew = TimeSpan.FromSeconds(30),
                          ValidateIssuerSigningKey = true,//是否验证SecurityKey
                          ValidAudience = Const.Domain,//Audience
                          ValidIssuer = Const.Domain,//Issuer，这两项和前面签发jwt的设置一致
                          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Const.SecurityKey))//拿到SecurityKey
                      };
                  });

```

获取jwt函数

```c#
[AllowAnonymous]
[HttpGet]
public IActionResult Get(string userName, string pwd)
{
    if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(pwd))
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
            new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeSeconds()}"),
            new Claim(ClaimTypes.Name, userName)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Const.SecurityKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: Const.Domain,
            audience: Const.Domain,
            claims: claims,
            expires: DateTime.Now.AddMinutes(1),
            signingCredentials: creds);

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token)
        });
    }
    else
    {
        return BadRequest(new { message = "username or password is incorrect." });
    }
}
```



请求：

```
http://localhost:5000/Passport?userName=name&pwd=123
```

```json
{
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOiIxNTkzNjY2Mzk3IiwiZXhwIjoxNTkzNjY2NDU3LCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibmFtZSIsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMCJ9.J3P6nHqtLRqXivBZrLExxzdxEGGhNNwhQDDflOm1vDY"
}
```

验证：

```
http://localhost:5000/weatherforecast
```

header:

```
Authorization:Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOiIxNTkzNjY2Mzk3IiwiZXhwIjoxNTkzNjY2NDU3LCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibmFtZSIsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMCJ9.J3P6nHqtLRqXivBZrLExxzdxEGGhNNwhQDDflOm1vDY
```



# 修改为每次请求刷新Token

添加拦截器：

```c#

/// <summary>
/// 在控制器执行之后调用
/// </summary>
/// <param name="context">执行的上下文</param>
public override void OnActionExecuted(ActionExecutedContext context)
{
    var isAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;

    if (isAuthenticated)
    {
    
        var jwtToken = TokenHelper.GetToken(new UserDto
        {
            Id = Guid.Parse(context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id").Value),
            Name = context.HttpContext.User.Identity.Name
        });
    
        context.HttpContext.Response.Headers.Add("Authorization", jwtToken);
        context.HttpContext.Response.Cookies.Append("Authorization", jwtToken);


    };

}
```



修改Startup.ConfigureServices 注册拦截器：



```c#
services.AddMvc(options =>
{
    options.Filters.Add(new Microsoft.AspNetCore.Mvc.Authorization.AuthorizeFilter());
    options.Filters.Add(typeof(PassportAttribute));

});
```



策略版在 分支：https://github.com/raikay/utility/tree/jwt-policy/Jwt.Demo.MS