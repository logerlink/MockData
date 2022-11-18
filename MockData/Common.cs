using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MockData
{
    public class Common
    {

        /// <summary>
        /// 获取guid
        /// </summary>
        /// <returns></returns>
        public static Guid GetGuid()
        {
            return Guid.NewGuid();
        }
        /// <summary>
        /// 获取ip
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            var list = Enumerable.Range(0, 4).Select(x => StaticData.GetRandom().Next(0, 256));
            return string.Join(".", list);
        }
        /// <summary>
        /// 获取bool值
        /// </summary>
        /// <returns></returns>
        public static bool GetFlag()
        {
            return StaticData.GetRandom().Next(1, 3) < 2;
        }

        #region 获取域名
        private static string[] _domainFixs = null;
        private static string[] GetDomainFixs()
        {
            if (_domainFixs == null || _domainFixs.Length <= 0) _domainFixs = StaticData.MAIN_FIX_DOMAIN.Split('、');
            return _domainFixs;
        }
        /// <summary>
        /// 获取域名
        /// </summary>
        /// <returns></returns>
        public static string GetDomain()
        {
            var pre = StaticData.GetRandom().Next(0, 10) > 2 ? "www." : "";
            var len = StaticData.GetRandom().Next(6, 12);
            var x = Word.GetWord(len, lang: Enums.Lang.EN);
            x = x.Replace(" ", "").ToLower();
            var value = GetDomainFixs();
            var xx = StaticData.GetRandom().Next(0, value.Count());
            return $"{pre}{x}.{value[xx]}";
        }
        #endregion
        /// <summary>
        /// 重复某段字符串
        /// </summary>
        /// <param name="source">指定字符串</param>
        /// <param name="min">最小重复次数</param>
        /// <param name="max">最大重复次数</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string RepeatStr(string source,int min = 3,int max = 10)
        {
            return string.Join("", RepeatArr(source, min, max));
        }
        /// <summary>
        /// 重复某个值，并转为集合返回
        /// </summary>
        /// <typeparam name="T">若T为引用类型（非string）则获取到的结果都会指向同一个引用地址</typeparam>
        /// <param name="source">指定值</param>
        /// <param name="min">最小重复次数</param>
        /// <param name="max">最大重复次数</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static IEnumerable<T> RepeatArr<T>(T source, int min = 3, int max = 10)
        {
            var xx = typeof(T);
            if (min < 0 || max < 0) throw new Exception("最大最小值不能小于0");
            if (max < min) throw new Exception("最大值不能小于最小值");
            var len = StaticData.GetRandom().Next(min, max);
            return Enumerable.Range(0, len).Select(X => source);
        }
        /// <summary>
        /// 从指定数据源中随机获取一批数据 会有重复数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">数据源</param>
        /// <param name="count">指定数量</param>
        /// <returns></returns>
        public static IEnumerable<T> GetRandomArr<T>(IEnumerable<T> source,int count)
        {
            var sourceCount = source.Count();
            if (count >= sourceCount) return source;
            var temp = source.Select(x => x).ToList();
            var result = Enumerable.Range(0, count).Select(x =>
            {
                var index = StaticData.GetRandom().Next(0, temp.Count);
                return temp[index];
            });
            return result;
        }
        /// <summary>
        /// 从指定数据源中随机获取一批数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">数据源</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public static IEnumerable<T> GetRandomArr<T>(IEnumerable<T> source, int min,int max)
        {
            var len = Number.Get(min, max);
            return GetRandomArr(source, len);
        }
        /// <summary>
        /// 从指定字符串数据源中随机获取一批数据
        /// </summary>
        /// <param name="source">字符串数据源</param>
        /// <param name="split">分隔符，没有分隔符则设为""</param>
        /// <param name="count">获取长度</param>
        /// <returns></returns>
        public static IEnumerable<string> GetRandomArr(string source,string split,int count)
        {
            var sourceList = new List<string>();
            if (string.IsNullOrWhiteSpace(split.ToString())) sourceList = source.ToCharArray().Select(x => x.ToString()).ToList();
            else sourceList = source.Split(split.ToCharArray()).ToList();
            return GetRandomArr(sourceList,count);
        }

    }
}
