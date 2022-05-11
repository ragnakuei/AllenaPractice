using ServicesLayer.ParkingFeeCalculators;

namespace TestProject.ParkingFeeTests.Q4;

public class ParkingFeeCalculatorTypeCTests
{
    [TestCase("2022/5/2 09:00:00", "2022/5/2 10:00:59", 20,  1)] // 同一天, 第一小時為20元
    [TestCase("2022/5/2 09:00:00", "2022/5/2 10:01:00", 50,  1)] // 同一天, 61分,第一小時20,第二小時起,每小時30
    [TestCase("2022/5/2 09:00:00", "2022/5/2 11:01:00", 80,  1)] // 2小又1分,第一小時20,第二小時起,每小時30
    [TestCase("2022/5/2 09:00:00", "2022/5/2 12:01:00", 110, 1)] // 3小又1分,第一小時20,第二小時起,每小時30
    public void 平日_同一天(DateTime from,
                       DateTime to,
                       int      expectedFee,
                       int      expectedDays)
    {
        AssertMethod(from, to, expectedFee, expectedDays);
    }

    [TestCase("2022/5/2 23:48:00", "2022/5/3 00:00:00", 20,  2)] // 跨一天,20 + 0
    [TestCase("2022/5/2 23:48:00", "2022/5/3 00:11:59", 40,  2)] // 跨一天,20 + 20
    [TestCase("2022/5/2 00:00:00", "2022/5/3 01:01:00", 350, 2)] // 跨一天,第二天1小時又1分, 300 + 50
    public void 平日_跨一天(DateTime from,
                       DateTime to,
                       int      expectedFee,
                       int      expectedDays)
    {
        AssertMethod(from, to, expectedFee, expectedDays);
    }

    [TestCase("2022/5/2 23:49:00", "2022/5/4 00:11:59", 340, 3)] // 跨2天,20 + 300 + 20
    public void 平日_跨2天(DateTime from,
                       DateTime to,
                       int      expectedFee,
                       int      expectedDays)
    {
        AssertMethod(from, to, expectedFee, expectedDays);
    }

    [TestCase("2022/5/6 23:58:00", "2022/5/7 00:01:00", 20, 2)]
    public void 平日_跨_假日(DateTime from,
                        DateTime to,
                        int      expectedFee,
                        int      expectedDays)
    {
        AssertMethod(from, to, expectedFee, expectedDays);
    }

    [TestCase("2022/5/8 23:58:00", "2022/5/9 00:01:00", 20, 2)]
    public void 假日_跨_平日(DateTime from,
                        DateTime to,
                        int      expectedFee,
                        int      expectedDays)
    {
        AssertMethod(from, to, expectedFee, expectedDays);
    }

    [TestCase("2022/5/7 00:00:00", "2022/5/7 23:59:59", 0, 1)] // 1分鐘,假日免費
    [TestCase("2022/5/7 00:00:00", "2022/5/8 23:59:59", 0, 2)] // 跨一天,假日免費
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
        var parkingFeeCalculator = new ParkingFeeCalculatorFactory().Get(nameof(ParkingFeeCalculatorV02),
                                                                         nameof(ParkingDailyFeeTypeCCalculator));
        var actual = parkingFeeCalculator.CalcParkingFee(from, to);

        Assert.AreEqual(expectedDays, actual.Items.Count);
        Assert.AreEqual(expectedFee,  actual.TotalFee);
    }
}
