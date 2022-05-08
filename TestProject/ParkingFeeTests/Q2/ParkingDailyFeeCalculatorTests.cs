using ServicesLayer.ParkingFeeCalculators;

namespace TestProject.ParkingFeeTests.Q2;

public class ParkingDailyFeeCalculatorTests
{
    [Test]
    [TestCase("00:00", "00:00:00")]
    [TestCase("00:00", "00:09:59")]
    public void 十分鐘內_為_0_元(DateTime from, DateTime to)
    {
        AssertMethod(from, to, 0);
    }

    [Test]
    [TestCase("00:00", "00:11")]
    [TestCase("00:00", "00:30")]
    public void 十一分鐘至三十分鐘_為_7_元(DateTime from, DateTime to)
    {
        AssertMethod(from, to, 7);
    }

    [Test]
    [TestCase("00:00", "00:31")]
    [TestCase("00:00", "00:59")]
    public void 三十一分鐘至五十九分鐘_為_10_元(DateTime from, DateTime to)
    {
        AssertMethod(from, to, 10);
    }

    [Test]
    [TestCase("00:00", "01:00", 10)]
    [TestCase("00:00", "02:00", 20)]
    [TestCase("00:00", "03:00", 30)]
    [TestCase("00:00", "04:00", 40)]
    [TestCase("00:00", "05:00", 50)]
    public void 每小時_為_10_元(DateTime from, DateTime to, int expected)
    {
        AssertMethod(from, to, expected);
    }

    [Test]
    [TestCase("00:00", "01:01")]
    [TestCase("00:00", "01:30")]
    public void 一小時_1分_至_小於等於30分_為_17_元(DateTime from, DateTime to)
    {
        AssertMethod(from, to, 17);
    }

    [Test]
    [TestCase("00:00", "01:31")]
    [TestCase("00:00", "01:59")]
    public void 一小時_大於30分_為_20_元(DateTime from, DateTime to)
    {
        AssertMethod(from, to, 20);
    }

    [Test]
    [TestCase("00:00", "05:01")]
    [TestCase("00:00", "23:59")]
    public void 大於等於5小時_為_50_元(DateTime from, DateTime to)
    {
        AssertMethod(from, to, 50);
    }

    private static void AssertMethod(DateTime from, DateTime to, int expected)
    {
        var parkingMinutesCalculator  = new ParkingMinutesCalculator();
        var parkingDailyFeeCalculator = new ParkingDailyFeeCalculator(parkingMinutesCalculator);
        var actual                    = parkingDailyFeeCalculator.Fee(from, to);

        Assert.AreEqual(expected, actual);
    }
}
