# 优惠券接口文档v2.1

## 1、获取用户名接口

接口地址:

`[POST]`:`http://testcoupon.iuoooo.com/Coupon/GetUserNameById`

请求参数：

```json
{
    "Id": "632bebea-b808-462f-8a2f-bb36e30b2820"
}
```

返回值：

```json
{
    "Data": {
        "ServerTime": "2020-04-08 10:39:35",
        "UserName": "16600000000"
    },
    "IsSuccess": true,
    "Info": "查询成功",
    "Code": 0,
    "IntField1": 0,
    "IntField2": 0,
    "StringField1": null,
    "StringField2": null,
    "ExtBag": null
}
```

字段说明：

| 字段       | 说明       |
| --| ---------- |
| Id         | 用户Id     |
| ServerTime | 服务器时间 |
| UserName   | 用户名称   |



## 2、核销优惠券接口

接口地址：

`[POST]`:`http://testcoupon.iuoooo.com/CouponTemplateManage/CodeVerification`

请求参数 ：

```json
{
    "AppId": "128b85de-0f43-4aac-bed9-33541f84eedb",
    "StoreId": "22745196-173c-47b8-8d62-7474df01640a",
    "UserId": "632bebea-b808-462f-8a2f-bb36e30b2820",
    "TotalAmount": 100,
    "ImgUrl": "http://cnd.img.com/img03.png",
    "CodeList": [
        "0F539D0937",
        "0F539D0937"
    ]
}
```

字段说明：

| 字段        | 说明                           |
| ----------- | ------------------------------ |
| ~~Code~~    | 这个字段已经废弃，改为CodeList |
| CodeList    | 券码数组                       |
| TotalAmount | 订单总额                       |
| ImgUrl      | 五定图片地址                   |



## 3、根据券码获取优惠券详情

接口地址：

`[POST]`:`http://testcoupon.iuoooo.com/Coupon/GetCouponDetailByCode`
请求参数：
```json
{
    "Code": "0F539D0937"
}
```
返回值：
```json
{
    "Data": {
        "Code": "KDH23985KDN",
        "Cash": 100,
        "LimitCondition": "满100元可用"
    },
    "IsSuccess": true,
    "Info": "查询成功",
    "Code": 0,
    "IntField1": 0,
    "IntField2": 0,
    "StringField1": null,
    "StringField2": null,
    "ExtBag": null
}
```

字段说明：

| 字段        | 说明                           |
| ----------- | ------------------------------ |
| Code        |  券码  |
| Cash    | 优惠券面值                       |
| LimitCondition | 优惠券适用条件                      |

## 4、核销记录接口

接口地址：

`[POST]`:`http://testcoupon.iuoooo.com/CouponTemplateManage/ValidationRecord`

请求参数：

```json
{
    "AppId": "128b85de-0f43-4aac-bed9-33541f84eedb",
    "StoreId": "22745196-173c-47b8-8d62-7474df01640a",
    "UserId": "632bebea-b808-462f-8a2f-bb36e30b2820",
    "PageNumber":1,
	"PageSize":10
}
```

返回值：

```json
{
    "Data": {
        "StoreTotalAmount": 2879,
        "StoreTotalCoupon": 28,
        "ValidationRecordItems": [
            {
                "ImgUrl": "http://cdn.img.com/imt03.png",
                "Codes": "277DEFCE60、277DEFCE60277DEFCE60",
                "TotalAmount": 300,
                "CouponAmount": 3.00,
                "Id": "1169acb6-5737-4096-b04b-0e97b418e642",
                "Name": "通用线下券",
                "ValidationTime": "2020-04-02 15:13:03",
                "ValidationDt": "/Date(1585811583857)/",
                "UserName": "c68ba6fd-81b7-4477-90b4-b70ecc2b5137"
            }
        ]
    },
    "IsSuccess": true,
    "Info": "查询成功",
    "Code": 0,
    "IntField1": 0,
    "IntField2": 0,
    "StringField1": null,
    "StringField2": null,
    "ExtBag": null
}
```

字段说明：

| 字段             | 说明             |
| ---------------- | ---------------- |
| StoreTotalAmount | 门店订单总金额   |
| StoreTotalCoupon | 门店优惠券总张数 |
| ImgUrl           | 五定图片         |
| Codes            | 券码             |
| TotalAmount      | 该订单消费总额   |
| CouponAmount     | 该订单优惠券总额 |
| Name             | 优惠券名称       |
| ValidationTime   | 核销时间         |
| UserName         | 用户名称         |

