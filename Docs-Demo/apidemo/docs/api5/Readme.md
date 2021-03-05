# 广告管理v1.6-编辑广告-选择商品-接口文档v1.0



## 1、获取商品列表

### 接口地址
【POST】https://testcoupon.iuoooo.com/CouponTemplateManage/GetCommoditys  

### 请求参数

```json
{
    "model": {
        "PageIndex": 1,
        "PageSize": 20,
        "AppId": "26623574-04f5-4a44-848e-84ea81677e16",
        "AppName": "",
        "CommodityName": "",
        "MaxRate": "",
        "MinRate": "",
        "MaxPrice": "",
        "MinPrice": "",
        "CatgoryIdList": [],
        "EsAppId": "26623574-04f5-4a44-848e-84ea81677e16"
    }
}
```



| 字段          | 说明       | 类型 |
| ------------- | ---------- | ---- |
| PageIndex     | 当前页     |      |
| PageSize      | 每页数量   |      |
| AppId         | AppId      |      |
| AppName       | App名称    |      |
| CommodityName | 商品名称   |      |
| MaxRate       | 最大毛利率 |      |
| MinRate       | 最小毛利率 |      |
| MaxPrice      | 最大价格   |      |
| MinPrice      | 最小价格   |      |
| CatgoryIdList | 分类       |      |
| EsAppId       | 馆Id       |      |




### 返回值
| 字段             | 说明                                              | 类型     |
| ---------------- | ------------------------------------------------- | -------- |
| MinRate          | 最小毛利率                                        | decimal  |
| MaxRate          | 最大毛利率                                        | decimal  |
| CategoryId       |                                                   | Guid     |
| GrossProfitRate  | 实际毛利率=（实际售价-当前进价）/实际售价         | decimal  |
| RealPrice        | 实际售价=当前售价-返油卡或易捷币可抵用金额        | decimal  |
| YjbPrice         | 易捷币抵现金额（商品当前售价*商品易捷币抵现比例） | decimal  |
| Id               |                                                   | Guid     |
| ReturnYjbPercent | 消费返易捷币比例                                  | decimal  |
| MaxPrice         | 最大价格                                          | decimal  |
| YouKaPercent     | 油卡兑换券比例                                    | decimal  |
| Stock            | 库存                                              | int      |
| Price            | 商品价格                                          | decimal  |
| CostPrice        | 商品进价                                          | decimal? |
| Pic              | 商品图片                                          | string   |
| JdCode           | 商品Jd编码                                        | string   |
| CommodityName    | 商品名称                                          | string   |
| CommodityId      | 商品ID                                            | Guid     |
| SupplierName     | 供应商名称                                        | string   |
| AppName          | 应用名称                                          | string   |
| AppId            | 应用ID                                            | Guid     |
| Percent          | 抵现比例                                          | decimal  |
| MinPrice         | 最小价格                                          | decimal  |

## 2、获取App列表

### 接口地址

【POST】https://testcoupon.iuoooo.com/CouponTemplateManage/GetApp

### 请求参数

```
{
    "PageIndex": 1,
    "pageSize": 10,
    "AppId": "26623574-04f5-4a44-848e-84ea81677e16"
}
```



### 返回值

```
{
    "data": [
        {
            "EsAppId": "26623574-04f5-4a44-848e-84ea81677e16",
            "EsAppName": "菜馆电商线1"
        }
    ],
    "count": 10
}
```

| 字段      | 说明   | 类型   |
| --------- | ------ | ------ |
| EsAppId   | 馆Id   | GUID   |
| EsAppName | 馆名称 | string |

