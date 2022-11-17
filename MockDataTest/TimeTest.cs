using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockDataTest
{
    internal class TimeTest
    {
        [Test]
        public void Action()
        {
            var x = MockData.Time.GetDateTime();
            var xx = MockData.Time.GetDateTimeOffset();
            var xxx = MockData.Time.GetDateTimeFormat(before: false);
            var xxxx = MockData.Time.GetTimeStamp();
            var xxxxx = MockData.Time.GetTimeStamp(false,false);
            var xxxxxx = MockData.Time.GetDateTimeFormat(format:"yyyy-MM-dd");
        }
    }
}
