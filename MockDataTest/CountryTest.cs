using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockDataTest
{
    public class CountryTest
    {
        [Test]
        public void CountryDataTest()
        {
            var city = MockData.Country.GetCity();
            var province = MockData.Country.GetProvince();
            var fullCity = MockData.Country.GetFullCity();
            var sw = new Stopwatch();
            sw.Start();
            var country = MockData.Country.GetCountry();
            sw.Stop();
            sw.Restart();
            var xx = MockData.Country.GetCountry();
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            var currency = MockData.Country.GetCurrencyCode();
            var countryCode = MockData.Country.GetCountryCode();
        }
    }
}
