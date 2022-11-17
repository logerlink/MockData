using System;
using System.Collections.Generic;
using System.Text;

namespace MockData
{
    public static class Time
    {
        /// <summary>
        /// 最大的时间随机秒数，100年 100*12*30*24*60*60
        /// </summary>
        const long MAX_DIFF_SECOND = 3110400000;
        /// <summary>
        /// 随机获取某个时间
        /// </summary>
        /// <param name="before">是否获取过去的时间</param>
        /// <returns></returns>
        public static DateTime GetDateTime(bool before = true)
        {
            var value = GetRandomValue(before);
            return DateTime.Now.AddSeconds(value);
        }
        /// <summary>
        /// 随机获取某个时间字符串
        /// </summary>
        /// <param name="before">是否获取过去的时间</param>
        /// <param name="format">时间格式</param>
        /// <returns></returns>
        public static string GetDateTimeFormat(string format = "", bool before = true)
        {
            if (string.IsNullOrWhiteSpace(format)) format = "yyyy-MM-dd HH:mm:ss";
            var value = GetRandomValue(before);
            return DateTime.Now.AddSeconds(value).ToString(format);
        }
        /// <summary>
        /// 随机获取某个时间含时区
        /// </summary>
        /// <param name="before">是否获取过去的时间</param>
        /// <returns></returns>
        public static DateTimeOffset GetDateTimeOffset(bool before = true)
        {
            var value = GetRandomValue(before);
            return DateTimeOffset.Now.AddSeconds(value);
        }
        /// <summary>
        /// 随机获取某个时间的时间戳
        /// </summary>
        /// <param name="before">是否获取过去的时间</param>
        /// <param name="isSecond">时间戳单位，默认秒——10个数，毫秒——13个数</param>
        /// <returns></returns>
        public static long GetTimeStamp(bool before = true,bool isSecond = true)
        {
            var value = GetRandomValue(before);
            var timeSpan = DateTime.Now.AddSeconds(value).ToUniversalTime() - new DateTime(1970, 1, 1);
            var diffValue = isSecond ? timeSpan.TotalSeconds : timeSpan.TotalMilliseconds;
            return (long)diffValue;
        }
        private static double GetRandomValue(bool before = true)
        {
            var value = Math.Round(StaticData.GetRandom().NextDouble() * MAX_DIFF_SECOND, 0);
            if (before) value = -value;
            return value;
        }

    }
}
