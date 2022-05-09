using ServicesLayer.ParkingFeeCalculators;

namespace TestProject.ParkingFeeTests.Q3;

public class ParkingFeeCalculatorTypeATests
{
    [TestCase("2022/5/1 09:00:00", "2022/5/1 09:10:59", 0)]  // 同一天
    [TestCase("2022/5/1 09:00:00", "2022/5/1 09:11:59", 7)]  // 同一天
    [TestCase("2022/5/1 09:00:00", "2022/5/1 10:00:59", 10)] // 同一天
    public void 同一天(DateTime from, DateTime to, int expected)
    {
        AssertMethod(from, to, expected);
    }

    [TestCase("2022/5/1 23:49:00", "2022/5/2 00:10:59", 0)]  // 跨一天,免費
    [TestCase("2022/5/1 23:48:00", "2022/5/2 00:00:00", 7)]  // 跨一天,收費		
    [TestCase("2022/5/1 23:48:00", "2022/5/2 00:11:59", 14)] // 跨一天,收費
    [TestCase("2022/5/1 00:00:00", "2022/5/2 00:11:59", 57)] // 跨一天,收費
    public void 跨一天(DateTime from, DateTime to, int expected)
    {
        AssertMethod(from, to, expected);
    }

    [TestCase("2022/5/1 23:49:00", "2022/5/3 00:11:59", 57)]  // 跨2天,收費
    [TestCase("2022/5/1 22:59:00", "2022/5/3 00:11:59", 67)]  // 跨2天,收費
    [TestCase("2022/5/1 00:00:00", "2022/5/3 00:11:59", 107)] // 跨2天,收費
    public void 跨2天(DateTime from, DateTime to, int expected)
    {
        AssertMethod(from, to, expected);
    }

    private static void AssertMethod(DateTime from, DateTime to, int expectedFee)
    {
        var parkingMinutesCalculator  = new ParkingMinutesCalculator();
        var parkingDailyFeeCalculator = new ParkingDailyFeeTypeACalculator(parkingMinutesCalculator);
        var parkingFeeCalculator      = new ParkingFeeCalculatorV02(parkingDailyFeeCalculator);
        var actual                    = parkingFeeCalculator.CalcParkingFee(from, to);

        Assert.AreEqual(expectedFee, actual.TotalFee);
    }
}
