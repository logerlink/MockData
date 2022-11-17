using MockData.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MockData
{
    public static class UserInfo
    {
        /// <summary>
        /// 获取全名
        /// </summary>
        /// <param name="lastLen"></param>
        /// <returns></returns>
        public static string GetFullName(int lastLen = 2,Lang lang = Lang.CN)
        {
            var result = string.Empty;
            switch (lang)
            {
                case Lang.CN:
                    result = GetFirstName(lang) + GetLastName(lastLen, lang);
                    break;
                case Lang.EN:
                    result = GetLastName(lastLen, lang) + " " + GetFirstName(lang);
                    break;
                case Lang.MIXIN:
                    result = "不支持该语言";
                    break;
                default:
                    result = "none";
                    break;
            }
            return result;
        }
        /// <summary>
        /// 获取姓氏
        /// </summary>
        /// <returns></returns>
        public static string GetFirstName(Lang lang = Lang.CN)
        {
            var result = string.Empty;
            switch (lang)
            {
                case Lang.CN:
                    {
                        var length = StaticData.LAST_NAME.Length;
                        var index = StaticData.GetRandom().Next(0, length + StaticData.LAST_NAME_DOUBLE.Length / 3);
                        if (index > length) return StaticData.LAST_NAME_DOUBLE.Split('、')[index - length];
                        index = StaticData.GetRandom().Next(0, length);
                        result = StaticData.LAST_NAME[index].ToString();
                    }
                    break;
                case Lang.EN:
                    {
                        var len = StaticData.GetRandom().Next(4, 8);
                        result = Word.GetWord(len, lang: lang).Replace(" ", "");
                    }
                    break;
                case Lang.MIXIN:
                    result = "不支持该语言";
                    break;
                default:
                    result =  "none";
                    break;
            }
            return result;
        }
        /// <summary>
        /// 获取名字，不含姓氏
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetLastName(int length = 2, Lang lang = Lang.CN)
        {
            var result = string.Empty;
            switch (lang)
            {
                case Lang.CN:
                    result = String.Join("", Word.GetChineneWord(length));
                    break;
                case Lang.EN:
                    {
                        var len = StaticData.GetRandom().Next(4, 12);
                        result = Word.GetWord(len, lang: lang).Replace(" ","");
                    }
                    break;
                case Lang.MIXIN:
                    result = "不支持该语言";
                    break;
                default:
                    result = "none";
                    break;
            }
            return result;
        }
        /// <summary>
        /// 获取身高
        /// </summary>
        /// <returns></returns>
        public static string GetTallStr() => GetTall() + "cm";
        /// <summary>
        /// 获取身高
        /// </summary>
        /// <returns></returns>
        public static decimal GetTall() => 120 + Math.Round((decimal)StaticData.GetRandom().NextDouble() * 100,0);
        /// <summary>
        /// 获取体重
        /// </summary>
        /// <returns></returns>
        public static string GetWeightStr() => GetWeight() + "kg";
        /// <summary>
        /// 获取体重
        /// </summary>
        /// <returns></returns>
        public static decimal GetWeight() => 40 + Math.Round((decimal)StaticData.GetRandom().NextDouble() * 60, 0);
        /// <summary>
        /// 获取邮件
        /// </summary>
        /// <returns></returns>
        public static string GetEmail()
        {
            int mail = StaticData.GetRandom().Next(5, 18);
            int value = StaticData.GetRandom().Next(3, 8);
            return $"{Word.GetRandomizer(mail,true,false,true,true)}@{Word.GetRandomizer(value, booSmallword:true)}.com";
        }
        /// <summary>
        /// 获取详细地址
        /// </summary>
        /// <returns></returns>
        public static string GetAddress()
        {
            int x = StaticData.GetRandom().Next(2, 4);
            int xx = StaticData.GetRandom().Next(2, 5);
            var xxx = StaticData.GetRandom().Next(0, 1000).ToString().PadLeft(4,'0');
            var xxxx = StaticData.GetRandom().Next(100, 5000).ToString().PadLeft(4, '0');
            return $"{string.Join("", Word.GetChineneWord(x))}街道{string.Join("", Word.GetChineneWord(xx))}路{xxx}号{xxxx}室";
        }
        /// <summary>
        /// 获取电话号码
        /// </summary>
        /// <returns></returns>
        public static string GetPhone()
        {
            int x = StaticData.GetRandom().Next(10, 1000);
            int xx = x < 100 ? StaticData.GetRandom().Next(10000000, 99999999) : StaticData.GetRandom().Next(1000000, 9999999);
            return $"0{x}-{xx}";
        }
        /// <summary>
        /// 获取手机号码
        /// </summary>
        /// <returns></returns>
        public static string GetTelPhone()
        {
            int xx = StaticData.GetRandom().Next(10000, 99999);
            int xxx = StaticData.GetRandom().Next(10000, 99999);
            return $"1{xx}{xxx}";
        }
        /// <summary>
        /// 获取邮编
        /// </summary>
        /// <returns></returns>
        public static string GetZipCode()
        {
            // "^[1-9]\\d{5}$";
            var x = StaticData.GetRandom().Next(1, 10);
            var y = Number.Get(10000,99999);
            return $"{x}{y}";
        }
        // 获取身份证
        public static string GetID(bool is18 = true)
        {
            // 行政区划6、出生年月8/6、数字3、数字(1-10)
            var x = StaticData.GetRandom().Next(110000,770000);
            var xx = Time.GetDateTimeFormat(is18 ? "yyyyMMdd" : "yyMMdd");
            var xxx = StaticData.GetRandom().Next(100,999);
            var y = StaticData.GetRandom().Next(0, 11);
            var yy = y == 10 ? "x" : y + "";
            return $"{x}{xx}{xxx}{yy}";
        }
    }
}
