# 纯天然手写JWT

> 微软对jwt有封装 idst等一系列 
> 这个项目主要是没用那些框架以及相关引用，完全原生代码写的  
> 主要是帮助理解jwt。  

# 什么是jwt？

JWT 英文名是 Json Web Token ，是一种用于通信双方之间传递安全信息的简洁的、URL安全的表述性声明规范，经常用在跨域身份验证。  

JWT 以 JSON 对象的形式安全传递信息。因为存在数字签名，因此所传递的信息是安全的。  

由三部分组成：

Header（头部）

Payload（数据）

Signature（签名）  

JWT=头部编码+数据编码+加密（头部编码+数据编码+key）

> 编码采用base64编码，加密采用头部alg声明编码方式加密


### 一：头部（Header）

JWT 头描述了 JWT 元数据，是一个 JSON 对象，它的格式如下：

```json
{"typ":"JWT","alg":"HS256"}
```
这里的 alg 属性表示签名所使用的算法，JWT 签名默认的算法为 HMAC SHA256 ， alg 属性值 HS256 就是 HMAC SHA256 算法。typ 属性表示令牌类型，这里就是 JWT。
Base64编码后：

```
eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9
```

### 二：载荷（playload）

有效载荷是 JWT 的主体，同样也是个 JSON 对象。有效载荷包含三个部分：
**1、标准注册声明：**
标准注册声明不是强制使用是的，但是我建议使用。它一般包括以下内容：
```
iss：jwt的签发者/发行人；
sub：主题；
aud：接收方；
exp：jwt过期时间；
nbf：jwt生效时间；
iat：签发时间
jti：jwt唯一身份标识，可以避免重放攻击
```
**2、公共声明：**
可以在公共声明添加任何信息，我们一般会在里面添加用户信息和业务信息，但是不建议添加敏感信息，因为公共声明部分可以在客户端解密。  

**3、私有声明：**
私有声明是服务器和客户端共同定义的声明，同样这里不建议添加敏感信息。  
下面这个代码段就是定义了一个有效载荷：  

```
{"sub":"1234567890","name":"John Doe","admin":true}
```
Base64编码：
```
eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWV9

```
### 三：签名（signature）

这个部分主要是确保数据不会被篡改，需要base64加密后的header和base64加密后的payload使用.连接组成的字符串，然后通过header中声明的加密方式进行加盐secret组合加密

c#代码如下：
```csharp
var hs256 = new System.Security.Cryptography.HMACSHA256(Encoding.ASCII.GetBytes(securityKey));

var encodedSignature = Convert.ToBase64String(hs256.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(encodedHeader, ".", encodedPayload))));
```

得到的Base64编码：  
```
TJVA95OrM7E2cBab30RMHrHDcEfxjoYZgeFONFh7HgQ
```
### 最终Jwt
三部分base64中间用点隔开拼接成一个字符串：
```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWV9.TJVA95OrM7E2cBab30RMHrHDcEfxjoYZgeFONFh7HgQ

```



#  如何应用？

需要请求头的 Authorization 字段中使用Bearer 模式添加 JWT，其内容看起来是下面这样：  

（Bearer和jwt之间有一个空格）

```
'Authorization': 'Bearer ' + token
```



# 注意事项！

在使用 JWT 时需要注意以下事项：

1. JWT 默认不加密，只是Base64编码，所以请勿存放敏感信息；

2. JWT 无法使服务器保存会话状态，当令牌生成后在有效期内无法取消也不能更改；

3. JWT 包含认证信息，如果泄露了，任何人都可以获得令牌所有的权限；因此 JWT 有效期不能太长，对于重要操作每次请求都必须进行身份验证。





## 项目请求示例：

**获取jwt token：**

```
[Get]http://localhost:5000/api/values/GetToken
```

```json
{
    "token": "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJyb2JlciIsImp0aSI6IjM0OWU0MGJkLWU5YTMtNDY4Yy04Y2IzLTYxMjc0YmJhODhjMiIsIm5iZiI6MTU5MzU4OTcwNCwiZXhwIjoxNTkzNTkxNTA0LCJpc3MiOiJyb2Jlcklzc3VlciIsImF1ZCI6InJvYmVyQXVkaWVuY2UiLCJhZ2UiOjMwfQ==.blLTsJt60pDF4NionCvfCgBYX66IfrUeyV5VVxl+FR0=",
    "code": 200,
    "message": "获取成功"
}
```

**验证：**

```
[Get]http://localhost:5000/api/values/Checktoken
```

Headers:

```
Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJyb2JlciIsImp0aSI6IjM0OWU0MGJkLWU5YTMtNDY4Yy04Y2IzLTYxMjc0YmJhODhjMiIsIm5iZiI6MTU5MzU4OTcwNCwiZXhwIjoxNTkzNTkxNTA0LCJpc3MiOiJyb2Jlcklzc3VlciIsImF1ZCI6InJvYmVyQXVkaWVuY2UiLCJhZ2UiOjMwfQ==.blLTsJt60pDF4NionCvfCgBYX66IfrUeyV5VVxl+FR0=
```
返回：
```
true
```

