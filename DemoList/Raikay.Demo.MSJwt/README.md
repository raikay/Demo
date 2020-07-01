这个demo可能是一个错误的演示，如果这样放在缓存，然后去校验缓存，和普通session一样了
不能分布式跨平台验证(排除分布式缓存)，如果验证方内存中没有就不能验证  

JWT 本来就是一种无状态的登录授权认证  
JWT是基于json的鉴权机制，而且是无状态的，服务器端是没有如传统那样保存客户端的登录信息的，这就为分布式开发提供了便利  



项目示例：

获取：

```
[Get]http://localhost:5000/api/WeatherForecast/GetToken
```

```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJBZG1pbiIsImp0aSI6ImRmYzk4OTEwLTZhNjgtNGRlMS1hNGI5LTRkOTUyZDljMzJiZiIsImlhdCI6IjIwMjAvNy8xIDc6NTA6MjYiLCJleHAiOjE1OTM2MzMwMjYsImlzcyI6IlJheVBJIiwiYXVkIjoi5byg5LiJIn0.nPYLbPony2M89gwNQooBTdTwm-l84PboTT6TGbO_aFg
```



验证：

```
[Get]http://localhost:5000/api/WeatherForecast/Get1
```



Headers:

```
Authorization:Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJBZG1pbiIsImp0aSI6ImRmYzk4OTEwLTZhNjgtNGRlMS1hNGI5LTRkOTUyZDljMzJiZiIsImlhdCI6IjIwMjAvNy8xIDc6NTA6MjYiLCJleHAiOjE1OTM2MzMwMjYsImlzcyI6IlJheVBJIiwiYXVkIjoi5byg5LiJIn0.nPYLbPony2M89gwNQooBTdTwm-l84PboTT6TGbO_aFg
```

```
[
    {
        "date": "2020-07-02T15:51:58.7220513+08:00",
        "temperatureC": -5,
        "temperatureF": 24,
        "summary": "Balmy"
    }
    ...
]
```

