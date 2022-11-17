using MockData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MockData
{
    public static class Word
    {
        /// <summary>
        /// 获取指定长度的文字
        /// </summary>
        /// <param name="length">指定长度</param>
        /// <param name="hasMark">是否有标点符号</param>
        /// <param name="lang">语言选择——中英文</param>
        /// <returns></returns>
        public static string GetWord(int length, bool hasMark = false, Lang lang = Lang.CN)
        {
            var markLength = 0;
            if (hasMark)
            {
                if (length < 10) markLength = 1;
                else markLength = length / 10;
            }
            var wordLength = length - markLength;
            var result = string.Empty;

            switch (lang)
            {
                case Lang.CN:
                    {
                        var strList = GetChineneWord(wordLength);
                        if (hasMark)
                        {
                            var marks = Enumerable.Range(0, markLength - 1).Select(x => x % 5 == 3 ? "。" : "，").ToList();

                            for (int i = 1; i < marks.Count; i++)
                            {
                                var item = marks[i];
                                var size = (strList.Count / (markLength - 1));
                                var x = (i - 1) * size + 1;
                                var y = i * size;
                                var index = StaticData.GetRandom().Next(x, y);
                                strList.Insert(index, item.ToString());
                            }

                            strList.Add("。");
                        }
                        result = string.Join("", strList);
                    }
                    break;
                case Lang.EN:
                    {
                        // xxx yyy rrrr,kkk,lll oo.
                        if (hasMark) length = length - 1;
                        var resultSB = new StringBuilder();
                        var hadMark = 0;
                        while (resultSB.Length < length)
                        {
                            var needLength = length - resultSB.Length;
                            // 创建随机文本，最长10
                            var createLength = needLength > 5 ? (needLength > 10 ? StaticData.GetRandom().Next(4, 10) : StaticData.GetRandom().Next(1, needLength)) : needLength;
                            var createWord = GetRandomizer(createLength, booSmallword: true);
                            if (resultSB.Length == 0) createWord = createWord[0].ToString().ToUpper() + createWord.Substring(1, createWord.Length - 1);
                            var isMark = StaticData.GetRandom().Next(0, 10) > 6 && hadMark < markLength && hasMark;
                            // ,.比例8:2
                            var spaceOrMark = isMark ? (StaticData.GetRandom().Next(0, 10) > 8 ? "." : ",") : " ";
                            resultSB.Append(createWord + spaceOrMark);
                        }
                        result = resultSB.ToString().Trim(',').Trim(' ');
                        if (hasMark) result = result + ".";
                    }
                    break;
                case Lang.MIXIN:
                    {
                        // 你好，marry你xx xxx是。
                        if (hasMark) length = length - 1;
                        var resultSB = new StringBuilder();
                        var hadMark = 0;
                        var isLastCnWord = true;
                        while (resultSB.Length < length)
                        {
                            var needLength = length - resultSB.Length;
                            // 创建随机文本，最长10
                            var createLength = needLength > 5 ? (needLength > 10 ? StaticData.GetRandom().Next(4, 10) : StaticData.GetRandom().Next(1, needLength)) : needLength;
                            // 中英比例：8:2
                            var isCnWord = StaticData.GetRandom().Next(10) > 2;
                            var createWord = isCnWord ? string.Join("", GetChineneWord(createLength)) : GetRandomizer(createLength, booSmallword: true);
                            if (resultSB.Length == 0) createWord = createWord[0].ToString().ToUpper() + createWord.Substring(1, createWord.Length - 1);
                            // 是否添加标点符号
                            var isMark = StaticData.GetRandom().Next(0, 10) > 6 && hadMark < markLength && hasMark;
                            var spaceOrMark = isMark ? "，" : "";
                            if (isMark && isLastCnWord)
                            {
                                // ，。比例8:2
                                spaceOrMark = StaticData.GetRandom().Next(0, 10) > 8 ? "。" : "，";
                                isLastCnWord = true;
                                hadMark++;
                            }
                            else if (!isLastCnWord && !isCnWord)
                            {
                                // 添加英文空格间隔
                                spaceOrMark = "";
                                isLastCnWord = isCnWord;
                                createWord = " " + createWord;
                            }
                            else
                            {
                                isLastCnWord = isCnWord;
                            }
                            resultSB.Append(createWord + spaceOrMark);
                        }
                        result = resultSB.ToString().Trim('。').Trim('，');
                        if (hasMark) result = result + "。";
                    }
                    break;
                default:
                    break;
            }

            return result;
        }
        
        /// <summary>
        /// 获取指定长度的文字并指定前后文字
        /// </summary>
        /// <param name="length">指定长度</param>
        /// <param name="before">文字前缀</param>
        /// <param name="after">文字后缀</param>
        /// <param name="hasMark">是否有标点符号</param>
        /// <param name="lang">语言选择——中英文</param>
        /// <returns></returns>
        public static string GetWordFormat(int length,string before="",string after="", bool hasMark = false, Lang lang = Lang.CN)
        {
            var x = GetWord(length,hasMark,lang);
            return $"{before}{x}{after}";
        }
        /// <summary>
        /// 获取标题
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static string GetTitle(Lang lang = Lang.MIXIN)
        {
            var length = StaticData.GetRandom().Next(30, 50);
            return GetWord(length,true,lang);
        }
        /// <summary>
        /// 获取一段文本内容
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static string GetContent(Lang lang = Lang.MIXIN)
        {
            var length = StaticData.GetRandom().Next(100, 500);
            return GetWord(length, true, lang);
        }
        /// <summary>
        /// 随机获取指定长度的 文本 集合
        /// </summary>
        /// <param name="count">指定长度</param>
        /// <param name="min">文本最小长度</param>
        /// <param name="max">文本最大长度</param>
        /// <param name="lang">语言</param>
        /// <param name="func">委托处理</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<string> GetWordArr(int count,int min = 3,int max = 10,Lang lang = Lang.MIXIN,Func<string,string> func = null)
        {
            if (min < 0 || max < 0) throw new Exception("文本最大最小值不能小于0");
            if(max < min) throw new Exception("文本最大值不能小于最小值");
            var range = Enumerable.Range(0, count).Select(x => {
                var len = StaticData.GetRandom().Next(min,max);
                var temp = GetWord(len, false, lang);
                if (func != null) temp = func(temp);
                return temp;
            }).ToList();
            return range;
        }
        /// <summary>
        /// 获取指定区间大小的数字并指定前后文字
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="before">文字前缀</param>
        /// <param name="after">文字后缀</param>
        /// <returns></returns>
        public static string GetNumberFormat(int min,int max, string before = "", string after = "")
        {
            var x = Number.Get(min,max);
            return $"{before}{x}{after}";
        }

        /// <summary>
        /// 获取指定区间大小的浮数字并指定前后文字
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="before">文字前缀</param>
        /// <param name="after">文字后缀</param>
        /// <returns></returns>
        public static string GetDecimalFormat(int min, int max, string before = "", string after = "",int decimals = 2)
        {
            var x = Number.GetDecimal(min, max, decimals);
            return $"{before}{x}{after}";
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="length">长度</param>
        /// <param name="mixin">是否包含大小写字母</param>
        /// <returns></returns>
        public static string GetCode(int length = 6,bool mixin = false)
        {
            return mixin ? GetRandomizer(length,true,false,true,true) : GetRandomizer(length, true);
        }
        /// <summary>
        /// 获取密码，大小写、字符、数字组合
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetPassword(int min = 8,int max = 16)
        {
            if (min < 8 || max < 8) throw new Exception("最大最小值不能小于8");
            if (max < min) throw new Exception("最大值不能小于最小值");
            var len = StaticData.GetRandom().Next(min, max);
            
            var xLen = StaticData.GetRandom().Next(2, len / 2);
            var yLen = StaticData.GetRandom().Next(1, len / 2);
            var zLen = len - xLen - yLen;
            var x = GetRandomizer(xLen,booSmallword:true);
            var y = string.Join("", Common.GetRandomArr("~,!,@,#,$,%,^,_", ",", yLen)); 
            var z = GetRandomizer(zLen, booNumber: true);
            var result = x.Substring(0, xLen / 2).ToUpper() + x.Substring(xLen / 2) + y + z;
            return String.Join("", result.ToCharArray().OrderBy(_ => Guid.NewGuid()));
        }


        /// <summary>
        /// 获取指定长度的中文字符串
        /// </summary>
        /// <param name="wordLength"></param>
        /// <returns></returns>
        internal static List<string> GetChineneWord(int wordLength)
        {
            //获取GB2312编码页（表） 
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding gb = Encoding.GetEncoding("gb2312");
            //调用函数产生4个随机中文汉字编码 
            object[] bytes = CreateRegionCode(wordLength);
            //根据汉字编码的字节数组解码出中文汉字 
            var strList = Enumerable.Range(0, wordLength).Select(x => gb.GetString((byte[])Convert.ChangeType(bytes[x], typeof(byte[])))).ToList();
            return strList;
        }
        /// <summary>
        /// 随机获取汉字
        /// 参考：https://www.cnblogs.com/cyberarmy/archive/2013/05/14/3077046.html ，非常感谢
        /// </summary>
        /// <param name="strlength"></param>
        /// <returns></returns>
        private static object[] CreateRegionCode(int strlength)
        {
            //定义一个字符串数组储存汉字编码的组成元素 
            string[] rBase = new String[16] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };

            Random rnd = StaticData.GetRandom();

            //定义一个object数组用来 
            object[] bytes = new object[strlength];

            /**/
            /*每循环一次产生一个含两个元素的十六进制字节数组，并将其放入bject数组中 
                每个汉字有四个区位码组成 
                区位码第1位和区位码第2位作为字节数组第一个元素 
                区位码第3位和区位码第4位作为字节数组第二个元素 
                */
            for (int i = 0; i < strlength; i++)
            {
                //区位码第1位 
                int r1 = rnd.Next(11, 14);
                string str_r1 = rBase[r1].Trim();

                //区位码第2位 
                rnd = new Random(r1 * unchecked((int)DateTime.Now.Ticks) + i);//更换随机数发生器的 

                //种子避免产生重复值 
                int r2;
                if (r1 == 13)
                {
                    r2 = rnd.Next(0, 7);
                }
                else
                {
                    r2 = rnd.Next(0, 16);
                }
                string str_r2 = rBase[r2].Trim();

                //区位码第3位 
                rnd = new Random(r2 * unchecked((int)DateTime.Now.Ticks) + i);
                int r3 = rnd.Next(10, 16);
                string str_r3 = rBase[r3].Trim();

                //区位码第4位 
                rnd = new Random(r3 * unchecked((int)DateTime.Now.Ticks) + i);
                int r4;
                if (r3 == 10)
                {
                    r4 = rnd.Next(1, 16);
                }
                else if (r3 == 15)
                {
                    r4 = rnd.Next(0, 15);
                }
                else
                {
                    r4 = rnd.Next(0, 16);
                }
                string str_r4 = rBase[r4].Trim();

                //定义两个字节变量存储产生的随机汉字区位码 
                byte byte1 = Convert.ToByte(str_r1 + str_r2, 16);
                byte byte2 = Convert.ToByte(str_r3 + str_r4, 16);
                //将两个字节变量存储在字节数组中 
                byte[] str_r = new byte[] { byte1, byte2 };

                //将产生的一个汉字的字节数组放入object数组中 
                bytes.SetValue(str_r, i);

            }

            return bytes;

        }
        /// <summary>
        /// 随机生成数字、符号、字母组合
        /// 参考：https://www.cnblogs.com/hanazawalove/p/6049790.html ，非常感谢
        /// </summary>
        /// <param name="intLength">指定长度</param>
        /// <param name="booNumber">是否包含数字</param>
        /// <param name="booSign">是否包含特殊符号</param>
        /// <param name="booSmallword">是否包含小写字母</param>
        /// <param name="booBigword">是否包含大写字母</param>
        /// <returns></returns>
        internal static string GetRandomizer(int intLength, bool booNumber = false, bool booSign = false, bool booSmallword = false, bool booBigword = false)
        {
            if (intLength <= 0 || (!booNumber && !booSign && !booSmallword && !booBigword)) return "";
            //定义
            Random ranA = StaticData.GetRandom();
            int intResultRound = 0;
            int intA = 0;
            string strB = "";
            while (intResultRound < intLength)
            {
                //生成随机数A，表示生成类型
                //1=数字，2=符号，3=小写字母，4=大写字母
                intA = ranA.Next(1, 5);
                //如果随机数A=1，则运行生成数字
                //生成随机数A，范围在0-10
                //把随机数A，转成字符
                //生成完，位数+1，字符串累加，结束本次循环
                if (intA == 1 && booNumber)
                {
                    intA = ranA.Next(0, 10);
                    strB = intA.ToString() + strB;
                    intResultRound = intResultRound + 1;
                    continue;
                }
                //如果随机数A=2，则运行生成符号
                //生成随机数A，表示生成值域
                //1：33-47值域，2：58-64值域，3：91-96值域，4：123-126值域
                if (intA == 2 && booSign == true)
                {
                    intA = ranA.Next(1, 5);
                    //如果A=1
                    //生成随机数A，33-47的Ascii码
                    //把随机数A，转成字符
                    //生成完，位数+1，字符串累加，结束本次循环
                    if (intA == 1)
                    {
                        intA = ranA.Next(33, 48);
                        strB = ((char)intA).ToString() + strB;
                        intResultRound = intResultRound + 1;
                        continue;
                    }

                    //如果A=2
                    //生成随机数A，58-64的Ascii码
                    //把随机数A，转成字符
                    //生成完，位数+1，字符串累加，结束本次循环
                    if (intA == 2)
                    {
                        intA = ranA.Next(58, 65);
                        strB = ((char)intA).ToString() + strB;
                        intResultRound = intResultRound + 1;
                        continue;
                    }

                    //如果A=3
                    //生成随机数A，91-96的Ascii码
                    //把随机数A，转成字符
                    //生成完，位数+1，字符串累加，结束本次循环
                    if (intA == 3)
                    {
                        intA = ranA.Next(91, 97);
                        strB = ((char)intA).ToString() + strB;
                        intResultRound = intResultRound + 1;
                        continue;
                    }

                    //如果A=4
                    //生成随机数A，123-126的Ascii码
                    //把随机数A，转成字符
                    //生成完，位数+1，字符串累加，结束本次循环
                    if (intA == 4)
                    {
                        intA = ranA.Next(123, 127);
                        strB = ((char)intA).ToString() + strB;
                        intResultRound = intResultRound + 1;
                        continue;
                    }
                }

                //如果随机数A=3，则运行生成小写字母
                //生成随机数A，范围在97-122
                //把随机数A，转成字符
                //生成完，位数+1，字符串累加，结束本次循环
                if (intA == 3 && booSmallword == true)
                {
                    intA = ranA.Next(97, 123);
                    strB = ((char)intA).ToString() + strB;
                    intResultRound = intResultRound + 1;
                    continue;
                }

                //如果随机数A=4，则运行生成大写字母
                //生成随机数A，范围在65-90
                //把随机数A，转成字符
                //生成完，位数+1，字符串累加，结束本次循环
                if (intA == 4 && booBigword == true)
                {
                    intA = ranA.Next(65, 89);
                    strB = ((char)intA).ToString() + strB;
                    intResultRound = intResultRound + 1;
                    continue;
                }
            }
            return strB;
        }
    }
}
