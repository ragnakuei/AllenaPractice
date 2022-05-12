using ServicesLayer.ValueObjects;

namespace TestProject.ParkingTimeTests.Q1;

public class ParkingTimeTests
{
    [Category("Value_只精確到分鐘")]
    [TestCase("2020/1/2 13:00:00",     "2020/1/2 13:00:00")]
    [TestCase("2020/1/2 13:00:59.999", "2020/1/2 13:00:00")]
    public void Value_只精確到分鐘(DateTime source, DateTime expected)
    {
        DateTime actual = new ParkingTime(source).Value;

        Assert.AreEqual(expected, actual);
    }

    [Category("支援與DateTime比大小")]
    [TestCase("2020/1/2 13:00:59.999", "2020/1/2 13:00:00")]
    public void Test_支援與DateTime比大小_相等_Returns0(DateTime source, DateTime other)
    {
        int actual = new ParkingTime(source).CompareTo(other);

        Assert.AreEqual(0, actual);
    }

    [Category("支援與DateTime比大小")]
    [TestCase("2020/1/2 13:01:00", "2020/1/2 13:00:00")]
    public void Test_支援與DateTime比大小_大於_Returns正數(DateTime source, DateTime other)
    {
        int actual = new ParkingTime(source).CompareTo(other);

        Assert.IsTrue(actual > 0);
    }

    [Category("支援與DateTime比大小")]
    [TestCase("2020/1/2 13:01:00", "2020/1/2 13:02:00")]
    public void Test_支援與DateTime比大小_大於_Returns負數(DateTime source, DateTime other)
    {
        int actual = new ParkingTime(source).CompareTo(other);

        Assert.IsTrue(actual < 0);
    }

    [Category("隱式轉型")]
    [Test]
    public void 隱式轉型_DateTime轉ParkingTime()
    {
        DateTime    source   = new DateTime(1980, 2, 5, 13, 0, 15); // 1980/2/5 13:00:15
        ParkingTime actual   = source;                              // 1980/2/5 13:00:00 只精確到分
        DateTime    expected = new DateTime(1980, 2, 5, 13, 0, 0);  // 1980/2/5 13:00:00

        Assert.AreEqual(expected, actual.Value);
    }

    [Category("隱式轉型")]
    [Test]
    public void 隱式轉型_ParkingTime轉DateTime()
    {
        DateTime    source   = new DateTime(1980, 2, 5, 13, 0, 15); // 1980/2/5 13:00:15
        ParkingTime actual   = source;                              // 1980/2/5 13:00:00 只精確到分
        DateTime    expected = new DateTime(1980, 2, 5, 13, 0, 0);  // 1980/2/5 13:00:00

        Assert.AreEqual(expected, (DateTime)actual);
    }

    [Category("運算子多載")]
    [TestCase("2020/1/2 13:00:00",     "2020/1/2 13:00:00")]
    [TestCase("2020/1/2 13:00:59.999", "2020/1/2 13:00:00")]
    public void 運算子多載_等於(DateTime source, DateTime other)
    {
        bool actual = (ParkingTime)source == (ParkingTime)other;

        Assert.IsTrue(actual);
    }

    [Category("運算子多載")]
    [TestCase("2020/1/2 13:01:00",     "2020/1/2 13:00:00")]
    [TestCase("2020/1/2 13:01:59.999", "2020/1/2 13:00:00")]
    public void 運算子多載_不等於(DateTime source, DateTime other)
    {
        bool actual = (ParkingTime)source != (ParkingTime)other;

        Assert.IsTrue(actual);
    }

    [Category("運算子多載")]
    [TestCase("2020/1/2 13:01:00",     "2020/1/2 13:00:00")]
    [TestCase("2020/1/2 13:01:59.999", "2020/1/2 13:00:00")]
    public void 運算子多載_大於(DateTime source, DateTime other)
    {
        bool actual = (ParkingTime)source > (ParkingTime)other;

        Assert.IsTrue(actual);
    }

    [Category("運算子多載")]
    [TestCase("2020/1/2 13:01:00",     "2020/1/2 13:00:00")]
    [TestCase("2020/1/2 13:01:59.999", "2020/1/2 13:00:00")]
    [TestCase("2020/1/2 13:00:00",     "2020/1/2 13:00:00")]
    [TestCase("2020/1/2 13:00:59.999", "2020/1/2 13:00:00")]
    public void 運算子多載_大於或等於(DateTime source, DateTime other)
    {
        bool actual = (ParkingTime)source >= (ParkingTime)other;

        Assert.IsTrue(actual);
    }

    [Category("運算子多載")]
    [TestCase("2020/1/2 13:01:00",     "2020/1/2 13:02:00")]
    [TestCase("2020/1/2 13:01:59.999", "2020/1/2 13:02:00")]
    public void 運算子多載_小於(DateTime source, DateTime other)
    {
        bool actual = (ParkingTime)source < (ParkingTime)other;

        Assert.IsTrue(actual);
    }

    [Category("運算子多載")]
    [TestCase("2020/1/2 13:01:00",     "2020/1/2 13:02:00")]
    [TestCase("2020/1/2 13:01:59.999", "2020/1/2 13:02:00")]
    [TestCase("2020/1/2 13:01:00",     "2020/1/2 13:01:59")]
    [TestCase("2020/1/2 13:01:59.999", "2020/1/2 13:01:30")]
    public void 運算子多載_小於或等於(DateTime source, DateTime other)
    {
        bool actual = (ParkingTime)source <= (ParkingTime)other;

        Assert.IsTrue(actual);
    }
}
