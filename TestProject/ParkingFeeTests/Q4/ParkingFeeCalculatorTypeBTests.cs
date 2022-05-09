using ServicesLayer.ParkingFeeCalculators;

namespace TestProject.ParkingFeeTests.Q4;

public class ParkingFeeCalculatorTypeBTests
{
    [TestCase("2022/5/2 09:00:00", "2022/5/2 09:10:59", 0,  1)] // 同一天
    [TestCase("2022/5/2 09:00:00", "2022/5/2 09:11:59", 7,  1)] // 同一天
    [TestCase("2022/5/2 09:00:00", "2022/5/2 10:00:59", 10, 1)] // 同一天
    public void 平日_同一天(DateTime from,
                       DateTime to,
                       int      expectedFee,
                       int      expectedDays)
    {
        AssertMethod(from, to, expectedFee, expectedDays);
    }

    [TestCase("2022/5/2 23:49:00", "2022/5/3 00:10:59", 0,  2)] // 跨一天,免費
    [TestCase("2022/5/2 23:48:00", "2022/5/3 00:00:00", 7,  2)] // 跨一天,收費
    [TestCase("2022/5/2 23:48:00", "2022/5/3 00:11:59", 14, 2)] // 跨一天,收費
    [TestCase("2022/5/2 00:00:00", "2022/5/3 00:11:59", 57, 2)] // 跨一天,收費
    public void 平日_跨一天(DateTime from,
                       DateTime to,
                       int      expectedFee,
                       int      expectedDays)
    {
        AssertMethod(from, to, expectedFee, expectedDays);
    }

    [TestCase("2022/5/2 23:49:00", "2022/5/4 00:11:59", 57,  3)] // 跨2天,收費
    [TestCase("2022/5/2 22:59:00", "2022/5/4 00:11:59", 67,  3)] // 跨2天,收費
    [TestCase("2022/5/2 00:00:00", "2022/5/4 00:11:59", 107, 3)] // 跨2天,收費
    public void 平日_跨2天(DateTime from,
                       DateTime to,
                       int      expectedFee,
                       int      expectedDays)
    {
        AssertMethod(from, to, expectedFee, expectedDays);
    }

    [TestCase("2022/5/6 23:59:00", "2022/5/7 00:01:00", 15, 2)]
    [TestCase("2022/5/6 23:48:00", "2022/5/7 00:01:00", 22, 2)]
    public void 平日_跨_假日(DateTime from,
                        DateTime to,
                        int      expectedFee,
                        int      expectedDays)
    {
        AssertMethod(from, to, expectedFee, expectedDays);
    }

    [TestCase("2022/5/8 23:58:00", "2022/5/9 00:11:00", 22, 2)]
    [TestCase("2022/5/8 23:58:00", "2022/5/9 00:10:00", 15, 2)]
    public void 假日_跨_平日(DateTime from,
                        DateTime to,
                        int      expectedFee,
                        int      expectedDays)
    {
        AssertMethod(from, to, expectedFee, expectedDays);
    }

    [TestCase("2022/5/7 09:00:00", "2022/5/7 09:01:59", 15,  1)] // 1分鐘,15元
    [TestCase("2022/5/7 09:00:00", "2022/5/7 10:00:59", 15,  1)] // 60分鐘,15元
    [TestCase("2022/5/7 09:00:00", "2022/5/7 09:00:59", 0,   1)] // 0分, 0元
    [TestCase("2022/5/7 09:00:00", "2022/5/7 10:01:59", 30,  1)] // 61分, 30元
    [TestCase("2022/5/7 23:58:00", "2022/5/8 00:01:00", 30,  2)] // 跨一天,收費
    [TestCase("2022/5/7 00:00:00", "2022/5/8 00:11:59", 265, 2)] // 250 + 15
    public void 假日(DateTime from,
                   DateTime to,
                   int      expectedFee,
                   int      expectedDays)
    {
        AssertMethod(from, to, expectedFee, expectedDays);
    }

    private static void AssertMethod(DateTime from,
                                     DateTime to,
                                     int      expectedFee,
                                     int      expectedDays)
    {
        var parkingMinutesCalculator  = new ParkingMinutesCalculator();
        var parkingDailyFeeCalculator = new ParkingDailyFeeTypeBCalculator(parkingMinutesCalculator);
        var parkingFeeCalculator      = new ParkingFeeCalculatorV02(parkingDailyFeeCalculator);
        var actual                    = parkingFeeCalculator.CalcParkingFee(from, to);

        Assert.AreEqual(expectedDays, actual.Items.Count);
        Assert.AreEqual(expectedFee,  actual.TotalFee);
    }
}
