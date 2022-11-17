using MockData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MockDataTest
{
    public class WordUnitTest
    {
        [Test]
        public void Get()
        {
            //var xx = new Regex("(.*?)\\((.*?)\\)").Match("2353532(顺丰到付)")?.Groups;
            //var yy = MockData.Word.GetRandomizer(50,booSign:true);
            var x = MockData.Word.GetWord(50,true,Lang.EN);
            var xx = MockData.Word.GetWordFormat(50,"-----","+++++");
            var xxx = MockData.Word.GetNumberFormat(10, 500, "=====", "=====");
            var xxxx = MockData.Word.GetDecimalFormat(10, 50, "￥");
            var title = MockData.Word.GetTitle();
            var content = MockData.Word.GetContent();
            var content1 = MockData.Word.GetContent(Lang.EN);
            var content2 = MockData.Word.GetContent(Lang.CN);
            var content3 = MockData.Word.GetTitle(Lang.EN);

            var code = MockData.Word.GetCode();
            var code1 = MockData.Word.GetCode(8,true);
            var password = MockData.Word.GetPassword();
            var password1 = MockData.Word.GetPassword(20,50);
            Console.WriteLine(x);
        }
    }
}
