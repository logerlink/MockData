namespace MockDataTest
{
    public class NumberUnitTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetTest()
        {
            var positiveNumber = MockData.Number.Get();
            Assert.IsTrue(positiveNumber > 0);
            var notPositiveNumber = MockData.Number.Get(false);
            Assert.IsTrue(notPositiveNumber < 0);
            var xx = MockData.Number.Get(10, 100);
            Assert.IsTrue(xx >= 10 && xx <= 100);

            var minNumber = MockData.Number.Get(10000);
            Assert.IsTrue(minNumber >= 10000);
        }
        [Test]
        public void GetArrTest()
        {
            var PArr = MockData.Number.GetArr(5);
            Assert.IsTrue(PArr.Count() == 5);
            var NPArr = MockData.Number.GetArr(50, false);
            Assert.IsTrue(NPArr.Count() == 50);

            var xx = MockData.Number.GetArr(100, 500, 1000);
            Assert.IsTrue(xx.Min() >= 500 && xx.Max() <= 1000 && xx.Count() == 100);
            var minNumberArr = MockData.Number.GetArr(66, 10000);
            Assert.IsTrue(minNumberArr.Min() >= 10000 && minNumberArr.Count() == 66);

            var repeatArr = MockData.Number.GetArr(6000, isRepeat: true);
            Assert.IsFalse(repeatArr.Count() == repeatArr.Distinct().Count());
            var noRepeatArr = MockData.Number.GetArr(10000);
            Assert.IsTrue(noRepeatArr.Count() == noRepeatArr.Distinct().Count());
        }

        [Test]
        public void GetDecimalTest()
        {
            var PNumber = MockData.Number.GetDecimal();
            Assert.IsTrue(PNumber > 0);
            var NPNumber = MockData.Number.GetDecimal(false);
            Assert.IsTrue(NPNumber < 0);


            var xx = MockData.Number.GetDecimal(50, 100);
            Assert.IsTrue(xx >= 50 && xx <= 100);
            var minNumber = MockData.Number.GetDecimal(1000);
            Assert.IsTrue(minNumber >= 1000);
        }

        [Test]
        public void GetDecimalArrTest()
        {
            var PArr = MockData.Number.GetDecimalArr(5);
            Assert.IsTrue(PArr.Count() == 5);
            var NPArr = MockData.Number.GetDecimalArr(50, false, 3);
            Assert.IsTrue(NPArr.Count() == 50);

            var xx = MockData.Number.GetDecimalArr(100, 500, 1000);
            Assert.IsTrue(xx.Min() >= 500 && xx.Max() <= 1000 && xx.Count() == 100);
            var minNumberArr = MockData.Number.GetDecimalArr(6600, 10000);
            Assert.IsTrue(minNumberArr.Min() >= 10000 && minNumberArr.Count() == 6600);
        }

        [Test]
        public void Test()
        {
            var x = MockData.Number.Get(10, 100);
            var xArr = MockData.Number.GetArr(3, 0, 10, false);
            
            var y = MockData.Number.GetDecimal(false);
            var yArr = MockData.Number.GetDecimalArr(3, 100, 500, 3);

        }
    }
}