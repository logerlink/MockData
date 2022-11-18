using MockData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MockData
{
    public static class Country
    {
        private static Dictionary<string, List<string>> _chinaCityDic = new Dictionary<string, List<string>>();
        private static Dictionary<string, List<string>> GetChinaCityDic()
        {
            if (_chinaCityDic == null || !_chinaCityDic.Any()) _chinaCityDic = StaticData.GetMainCity();
            return _chinaCityDic;
        }

        #region 随机获取国家
        private static string[] _countrys = null;
        private static string[] GetCountrys()
        {
            if (_countrys == null || _countrys.Length <= 0) _countrys = StaticData.MAIN_COUNTRY.Split('、');
            return _countrys;
        }
        /// <summary>
        /// 随机获取国家
        /// </summary>
        /// <returns></returns>
        public static string GetCountry()
        {
            var list = GetCountrys();
            var value = StaticData.GetRandom().Next(0, list.Count());
            return list[value];
        }
        #endregion
        #region 随机获取国家代码
        private static string[] _countryCodes = null;
        private static string[] GetCountryCodes()
        {
            if (_countryCodes == null || _countryCodes.Length <= 0) _countryCodes = StaticData.MAIN_COUNTRY_CODE.Split('、');
            return _countryCodes;
        }
        /// <summary>
        /// 随机获取国家代码
        /// </summary>
        /// <returns></returns>
        public static string GetCountryCode()
        {
            var list = GetCountryCodes();
            var value = StaticData.GetRandom().Next(0, list.Count());
            return list[value];
        }
        #endregion
        #region 随机获取国家货币代码
        private static string[] _currencyCodes = null;
        private static string[] GetCurrencyCodes()
        {
            if (_currencyCodes == null || _currencyCodes.Length <= 0) _currencyCodes = StaticData.MAIN_CURRENCY_CODE.Split('、');
            return _currencyCodes;
        }
        /// <summary>
        /// 随机获取货币代码
        /// </summary>
        /// <returns></returns>
        public static string GetCurrencyCode()
        {
            var list = GetCurrencyCodes();
            var value = StaticData.GetRandom().Next(0, list.Count());
            return list[value];
        }
        #endregion

        /// <summary>
        /// 随机获取省份
        /// </summary>
        /// <returns></returns>
        public static string GetProvince()
        {
            GetChinaCityDic();
            var value = StaticData.GetRandom().Next(0, _chinaCityDic.Count);
            return _chinaCityDic.Keys.ToList()[value];
        }
        /// <summary>
        /// 随机获取城市
        /// </summary>
        /// <param name="province"></param>
        /// <returns></returns>
        public static string GetCity(string province = "")
        {
            GetChinaCityDic();
            if (string.IsNullOrWhiteSpace(province)) province = GetProvince();
            if (!_chinaCityDic.ContainsKey(province)) return "";
            var list = _chinaCityDic[province];
            if (!list.Any()) return "";
            var value = StaticData.GetRandom().Next(0, list.Count);
            return list[value];
        }
        /// <summary>
        /// 随机获取省份和城市
        /// </summary>
        /// <returns></returns>
        public static string GetFullCity()
        {
            var x = GetProvince();
            var xx = GetCity(x);
            return $"{x}-{xx}";
        }
    }
}
