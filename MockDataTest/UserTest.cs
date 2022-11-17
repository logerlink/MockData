using MockData;
using MockData.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockDataTest
{
    public class UserTest
    {
        [Test]
        public void Test()
        {
            var addressLen = new Random().Next(1, 3);
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
            var tempStr = JsonConvert.SerializeObject(temp);
        }
    }
}
