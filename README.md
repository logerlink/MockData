# MockData.Net
MockData.Net——.net mock data，协助开发人员快速构建虚拟数据

#### 安装使用

```shell
dotnet add package MockData.Net
```

更多下载方式：[NuGet Gallery | MockData.Net](https://www.nuget.org/packages/MockData.Net)

使用文档：[MockData.Net使用文档](https://logerlink.github.io/page/2022/MockDataNet.html)

#### 简单示例

![image-20221118180004244](https://s2.loli.net/2022/11/18/eJQv45oqErW8tnx.png)

代码示例

```csharp
            var temp = new
            {
                Id = Common.GetGuid(),
                WebSite = Common.GetDomain(),
                RepeatNumber = Common.RepeatArr(3.14, max: 5).ToList(),
                Friends = Common.GetRandomArr("小明、小虎、筱筱、西西", "、", 2).ToList(),
                Name = UserInfo.GetFullName(),
                EName = UserInfo.GetLastName(lang: Lang.EN),
                IDCard = UserInfo.GetID(),
                Email = UserInfo.GetEmail(),
                TelPhone = UserInfo.GetTelPhone(),
                CreateTime = Time.GetDateTime(),
                BirthDay = Time.GetDateTimeFormat("MM-dd"),
                Content = Word.GetContent(Lang.EN),
                Description = Word.GetWord(100, true),
                FavoriteBooks = Word.GetWordArr(3, lang: Lang.CN, func: x => $"《{x ?? ""}》"),
                Price = Word.GetDecimalFormat(30, 50, "￥"),
                Code = Word.GetCode(),
                Password = Word.GetPassword(),
                HistoryPrices = Number.GetDecimalArr(5, 30, 50).ToList(),
                Address = Enumerable.Range(0, addressLen).Select(y => new
                {
                    Country = Country.GetCountryCode(),
                    Province = Country.GetProvince(),
                    City = Country.GetCity(),
                    Detail = UserInfo.GetAddress(),
                    ZipCode = UserInfo.GetZipCode(),
                    Phone = UserInfo.GetPhone()
                }).ToList()
            };
```
结果示例
```json
{
	"Id": "3f90cb30-22da-4748-87e7-59626bded4bd",
	"WebSite": "www.gubspiuewg.cn",
	"RepeatNumber": [3.14, 3.14, 3.14, 3.14],
	"Friends": ["筱筱", "筱筱"],
	"Name": "时榆主",
	"EName": "Ffuurq",
	"IDCard": "598497196209296647",
	"Email": "81T4Hiown43e@hpgj.com",
	"TelPhone": "17926442285",
	"CreateTime": "2006-03-03T00:38:43.3772615+08:00",
	"BirthDay": "02-10",
	"Content": "Cphxlqrjr.jqvcqm wsxv ivsroso tdprxix mgeg......unxt bc iufe.",
	"Description": "付库芝疮址狼蘑脏喧，嵌谚瓜吭么臂铜，哈疲神儡......读颊。",
	"FavoriteBooks": ["《响酉遇》", "《饭净勤井螺》", "《庸蟹徒莱贤蜗层龙》"],
	"Price": "￥49.51",
	"Code": "913846",
	"Password": "77gqU77$1",
	"HistoryPrices": [46.30, 45.98, 45.53, 47.64, 49.08],
	"Address": [{
		"Country": "IS",
		"Province": "广东省",
		"City": "",
		"Detail": "沫蝇丫街道秀狮颁操路0860号0749室",
		"ZipCode": "678966",
		"Phone": "0336-1755328"
	}, {
		"Country": "IN",
		"Province": "香港",
		"City": "",
		"Detail": "瓶巡哺街道淑山咒路0569号0500室",
		"ZipCode": "649002",
		"Phone": "0689-7036985"
	}]
}
```



































































