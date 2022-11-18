using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockDataTest
{
    public class CommentTest
    {
        [Test]
        public void Test()
        {
            var x = MockData.Common.GetGuid();
            var xx = MockData.Common.GetIP();
            var xxx = MockData.Common.GetFlag();
            var xxxx = MockData.Common.GetDomain();

            var y = MockData.Common.RepeatStr("Hello");
            var yy = MockData.Common.RepeatArr(555);
            var yyy = MockData.Common.RepeatArr(new A() { Age = 18 }).ToList();
            yyy[0].Age = 58;
            var yAge = yyy[1].Age;  //58
        }

        public class A
        {
            public int Age { get; set; }
        }

        [Test]
        public void GetRandomTest()
        {
            var x = MockData.Common.GetRandomArr(Enumerable.Range(0, 100), 5).ToList();
            var y = MockData.Common.GetRandomArr("Hello World!", "", 5).ToList();
        }
    }
}
