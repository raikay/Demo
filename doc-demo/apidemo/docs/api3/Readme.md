# 优惠券接口文档v3.3



v3.3更新内容：

- 获取领取记录接口新增BindUserAccount、Code

v3.2更新内容：

- PC端导出核销记录 接口 参数改为couponTemplateId
- 新增 【4、优惠券列表返回字段说明】



## 1、移动端导出核销记录

接口地址:

`[Get]`:`http://testcoupon.iuoooo.com/CouponTemplateManage/ExportValidationRecords?AppId=C45717BA-F13E-4065-9F04-914944B8D9AD&StoreId=C45717BA-F13E-4065-9F04-914944B8D9AD`



字段说明：

| 字段       | 说明       |
| --| ---------- |
| AppId    | 管Id |
| StoreId | 门店Id |

## 2、PC端导出核销记录

接口地址:

`[Get]`:`http://testcoupon.iuoooo.com/CouponTemplateManage/ExportValidationRecordsofpc?couponTemplateId=C45717BA-F13E-4065-9F04-914944B8D9AD`



字段说明：

| 字段       | 说明       |
| --| ---------- |
| couponTemplateId | 券Id |

## 3、获取领取记录接口

接口地址：

`[POST]`:`http://testcoupon.iuoooo.com/CouponTemplateManage/GetCouponUsedInfo`

请求参数：

```js
couponTemplateId:0A470E25-AC23-60AD-7586-10B626F29384
_search: false
nd: 1586946924535
rows: 20
page: 1
sidx: 
sord: asc
ReceiveBeginTime:2020-4-15 18:50:29
ReceiveEndTime:2020-4-15 18:50:39
WriteOffBeginTime:2020-4-15 18:50:39
WriteOffEndTime:2020-4-15 18:50:39
WriteOffEndTime:16600000000
```



字段说明：

| 字段              | 说明         |
| ----------------- | ------------ |
| ReceiveBeginTime  | 领用开始时间 |
| ReceiveEndTime    | 领用结束时间 |
| WriteOffBeginTime | 核销开始时间 |
| WriteOffEndTime   | 核销结束时间 |
| KeyWord           | 搜索关键词   |
| couponTemplateId  | 模板Id       |

返回值：

```json
{
    "page":1,
    "total":1,
    "records":3,
    "rows":[
        {
            "id":null,
            "cell":[
                "2020/4/15 8:51:52",
                "已使用",
                "16600000001",
                null,
                null,
                "9271d3ee-aa50-40cb-b7b2-9a05a2d4f29e",
                "3c67952d-e690-435e-ac66-5d1da2917952",
                "0a470e25-ac23-60ad-7586-10b626f29384",
                "a8b8cd40-84d6-4e11-aa12-179ae9e7d8ac",
                "2020-04-15 08:52:38",
                "零鸭蛋",
                "16600000000",
                "私房菜"
            ]
        }
    ],
    "userdata":""
}
```

字段说明

| 序号 | 字段                                 | 值                                   | 说明                     |
| ------ | ------------------------------ | ------------------------ | ------------------------ |
| 0    | BindTime        | "2020/4/15 8:51:52"                  | 绑定时间                 |
| 1    | IsUsedText           | "已使用"                             | 是否已使用               |
| 2    | BindUserName  | "16600000001"                        | 领用优惠券的用户的用户名 |
| 3    | BindUserPhone | null                                 | 领用优惠券的用户的手机   |
| 4    | BindUserEmail       | null                                 | 领用优惠券的用户的邮箱   |
| 5    | BindUserId | 9271d3ee-aa50-40cb-b7b2-9a05a2d4f29e | 绑定的用户ID             |
| 6    | CouponId | 3c67952d-e690-435e-ac66-5d1da2917952 | 优惠券Id                 |
| 7    | CouponTemplateId | 0a470e25-ac23-60ad-7586-10b626f29384 | 优惠券模版ID             |
| 8    | OrderId | a8b8cd40-84d6-4e11-aa12-179ae9e7d8ac | 使用该优惠券的订单号     |
| 9    | WriteOffTimeStr |   "2020-04-15 08:52:38"        | 核销时间                 |
| 10   | WriteOffUserName |  零鸭蛋                         | 核销人              |
| 11   | WriteOffUserAccount | 16600000000                 | 核销账号                 |
| 12  | WriteOffStoreName | 私房菜                               | 核销店铺                 |
| 13 | BindUserAccount |  | 领用人账号 |
| 14 | Code |  | 券码 |

​     



## 4、优惠券列表返回字段说明

https://testcoupon.iuoooo.com/CouponTemplateManage/CouponTemplateRecords

| 序号 | 字段             | 说明                     |
| ---- | ---------------- | ------------------------ |
| 0   | Name             | 优惠券模板名称           |
| 1   | Cash             | 金额                     |
| 2   | Count            | 发行量                   |
| 3   | ReceivedCount    | 已领取                   |
| 4   | BeginTime        | 生效日期                 |
| 5   | OperationName    | 操作名称                 |
| 6   | Gid              | 记录Id                   |
| 7   | CheckName        | 查看名称                 |
| 8   | EndTime          | 失效日期                 |
| 9  | Status           | 状态                     |
| 10  | OperationType    | 操作类型                 |
| 11  | UseType          | 使用类型                 |
| 12  | AppId            | AppId                    |
| 13  | PovType          | 过期类型                 |
| 14  | PeriodOfValidity | 优惠券过期时间(单位：天) |
| 15  | UseCount         | 已使用的                 |
| 16  | CouponType         | 线上券：0/1/2  线下 3/4                 |

