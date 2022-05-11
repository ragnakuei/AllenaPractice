using ServicesLayer.ParkingFeeCalculators;

namespace TestProject.ParkingFeeTests.Q1;

public class ParkingMinutesCalculatorTests
{
    [Test]
    [TestCase("00:01:00", "00:00:00")]
    public void From_晚於_To(DateTime from, DateTime to)
    {
        AssertMethod(from, to, null);
    }

    [Test]
    [TestCase("00:00:00", "00:00:00")]
    public void From_等於_To(DateTime from, DateTime to)
    {
        AssertMethod(from, to, 0);
    }

    [Test]
    [TestCase("00:00:00",         "00:00:59")]
    [TestCase("00:00:00.0000000", "00:00:00.0000001")]
    [TestCase("00:00:00.0000000", "00:00:59.9999999")]
    public void 同分不同秒(DateTime from, DateTime to)
    {
        AssertMethod(from, to, 0);
    }

    [Test]
    [TestCase("00:00:00", "00:01:59")]
    public void 差一分不同秒(DateTime from, DateTime to)
    {
        AssertMethod(from, to, 1);
    }

    private static void AssertMethod(DateTime from, DateTime to, int? expected)
    {
        IParkingMinutesCalculator parkingMinutesCalculator = new ParkingMinutesCalculator();

        var actual = parkingMinutesCalculator.Minutes(from, to);

        Assert.AreEqual(expected, actual);
    }
}
