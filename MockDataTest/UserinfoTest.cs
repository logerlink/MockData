using MockData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockDataTest
{
    public class UserinfoTest
    {
        [Test]
        public static void InfoTest()
        {
            var firstName = MockData.UserInfo.GetFirstName();
            var lastName = MockData.UserInfo.GetLastName();
            var fullName = MockData.UserInfo.GetFullName();
            var firstName1 = MockData.UserInfo.GetFirstName(lang: Lang.EN);
            var lastName1 = MockData.UserInfo.GetLastName(lang: Lang.EN);
            var fullName1 = MockData.UserInfo.GetFullName(lang: Lang.EN);
            var tall = MockData.UserInfo.GetTall();
            var tallStr = MockData.UserInfo.GetTallStr();
            var weight = MockData.UserInfo.GetWeight();
            var weightStr = MockData.UserInfo.GetWeightStr();
            var email = MockData.UserInfo.GetEmail();
            var tel = MockData.UserInfo.GetPhone();
            var phone = MockData.UserInfo.GetTelPhone();
            var address = MockData.UserInfo.GetAddress();
            var zipCode = MockData.UserInfo.GetZipCode();
            var id = MockData.UserInfo.GetID();
        }

        [Test]
        public static void TestZero()
        {
            // 0-9 包括0不包括10
            var xx = new Random().Next(10);
        }
    }
}
