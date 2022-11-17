using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockData
{
    /// <summary>
    /// min：-100000，max：100000
    /// include min but max
    /// </summary>
    public static class Number
    {
        #region const
        /// <summary>
        /// 正数最低界限
        /// </summary>
        private const int Number_1 = 1;
        /// <summary>
        /// 负数最高界限
        /// </summary>
        private const int Number__1 = -1;
        
        #endregion
        /// <summary>
        /// 随机获取一个数字，区间min-max
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Get(int min, int max)
        {
            var count = max - min;
            if (count < 0) throw new Exception("最大值不能小于最小值，请尝试调整min/max值");
            var range = Enumerable.Range(min, count);
            var index = StaticData.GetRandom().Next(max - min);
            return range.Skip(index - 1).Take(1).FirstOrDefault();
        }
        /// <summary>
        /// 随机获取一个数字，区间min-
        /// </summary>
        /// <param name="min"></param>
        /// <returns></returns>
        public static int Get(int min)
        {
            return Get(min, StaticData.NUMBER_MAX - min);
        }
        /// <summary>
        /// 随机获取一个(正/负)数字，不会得到0
        /// </summary>
        /// <param name="isPositive">正数</param>
        /// <returns></returns>
        public static int Get(bool isPositive = true)
        {
            return isPositive ? Get(Number_1, StaticData.NUMBER_MAX) : Get(StaticData.NUMBER_MIN, Number__1);
        }
        /// <summary>
        /// 随机获取指定长度的 整数 集合
        /// </summary>
        /// <param name="count">指定长度</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="isRepeat">是否允许重复数据</param>
        /// <returns></returns>
        public static IEnumerable<int> GetArr(int count, int min, int max, bool isRepeat = true)
        {
            var rangeCount = max - min;
            if (rangeCount < count) throw new Exception("总长度少于指定长度，无法获取，请尝试调整min/max值");
            var range = Enumerable.Range(min, rangeCount);
            var list = new ConcurrentBag<int>();
            // 多线程可能会导致数据重复
            var plr = Parallel.For(0, count,new ParallelOptions() { MaxDegreeOfParallelism = isRepeat ? 10 : 2 }, _ =>
            {
                var index = StaticData.GetRandom().Next(rangeCount);
                var item = isRepeat ? range.Skip(index).FirstOrDefault() : range.Skip(index).Where(x => !list.Contains(x)).FirstOrDefault();
                
                list.Add(item);
            });
            while (!plr.IsCompleted)
            {
                Task.Delay(200).Wait();
            }
            return list;
        }
        /// <summary>
        /// 随机获取指定长度的 整数 集合
        /// </summary>
        /// <param name="count">指定长度</param>
        /// <param name="min">最小值</param>
        /// <param name="isRepeat">是否允许重复数据</param>
        /// <returns></returns>
        public static IEnumerable<int> GetArr(int count, int min, bool isRepeat = true)
        {
            return GetArr(count, min, StaticData.NUMBER_MAX - min, isRepeat);
        }
        /// <summary>
        /// 随机获取指定长度的 整数 集合
        /// </summary>
        /// <param name="count">指定长度</param>
        /// <param name="isPositive">是否正数</param>
        /// <param name="isRepeat">是否允许重复数据</param>
        /// <returns></returns>
        public static IEnumerable<int> GetArr(int count, bool isPositive = true, bool isRepeat = true)
        {
            return isPositive ? GetArr(count, Number_1, StaticData.NUMBER_MAX, isRepeat) : GetArr(count, StaticData.NUMBER_MIN, Number__1, isRepeat);
        }
        /// <summary>
        /// 随机获取一个小数
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public static decimal GetDecimal(int min, int max, int decimals = 2)
        {
            var count = max - min;
            var range = Enumerable.Range(min, count);
            var index = StaticData.GetRandom().Next(max - min);
            var first = range.Skip(index).FirstOrDefault();
            // random 0-10
            // StaticData.GetRandom().NextDouble() * 10
            return GetDecimalValue(first, max, decimals);
        }
        private static decimal GetDecimalValue(int origin, int max, int decimals)
        {
            return Math.Round(origin + (decimal)StaticData.GetRandom().NextDouble() * (max - origin), decimals);
        }
        /// <summary>
        /// 随机获取一个小数
        /// </summary>
        /// <param name="min">最小值</param>
        /// <returns></returns>
        public static decimal GetDecimal(int min)
        {
            return GetDecimal(min, StaticData.NUMBER_MAX - min, 2);
        }
        /// <summary>
        /// 随机获取一个小数
        /// </summary>
        /// <param name="isPositive">是否正数</param>
        /// <returns></returns>
        public static decimal GetDecimal(bool isPositive = true, int decimals = 2)
        {
            return isPositive ? GetDecimal(Number_1, StaticData.NUMBER_MAX, decimals) : GetDecimal(StaticData.NUMBER_MIN, Number__1, decimals);
        }
        /// <summary>
        /// 随机获取指定长度 小数 集合 可能有重复数据
        /// </summary>
        /// <param name="count">指定长度</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public static IEnumerable<decimal> GetDecimalArr(int count, int min, int max, int decimals = 2)
        {
            var rangeCount = max - min;
            if (rangeCount < count) throw new Exception("总长度少于指定长度，无法获取，请尝试调整min/max值");
            var range = Enumerable.Range(min, rangeCount);
            var list = new ConcurrentBag<decimal>();
            var plr = Parallel.For(0, count, _ =>
            {
                var index = StaticData.GetRandom().Next(rangeCount);
                var item = range.Skip(index).Where(x => !list.Contains(x)).FirstOrDefault();
                list.Add(GetDecimalValue(item, max, decimals));
            });
            while (!plr.IsCompleted)
            {
                Task.Delay(200).Wait();
            }
            return list;
        }
        /// <summary>
        /// 随机获取指定长度 小数 集合
        /// </summary>
        /// <param name="count">指定长度</param>
        /// <param name="min">最小值</param>
        /// <returns></returns>
        public static IEnumerable<decimal> GetDecimalArr(int count, int min)
        {
            return GetDecimalArr(count, min, StaticData.NUMBER_MAX - min, 2);
        }
        /// <summary>
        /// 随机获取指定长度 小数 集合
        /// </summary>
        /// <param name="count">指定长度</param>
        /// <param name="isPositive">是否正数</param>
        /// <returns></returns>
        public static IEnumerable<decimal> GetDecimalArr(int count, bool isPositive = true, int decimals = 2)
        {
            return isPositive ? GetDecimalArr(count, Number_1, StaticData.NUMBER_MAX, decimals) : GetDecimalArr(count, StaticData.NUMBER_MIN, Number__1, decimals);
        }
    }
}
