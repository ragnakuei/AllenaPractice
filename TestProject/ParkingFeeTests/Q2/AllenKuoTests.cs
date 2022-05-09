using ServicesLayer.ParkingFeeCalculators;

namespace TestProject.ParkingFeeTests.Q2;

public class AllenKuoTests
{
    [TestCase("09:00:00", "09:00:00", 0)]  // [0,10]
    [TestCase("09:00:00", "09:10:59", 0)]  // [0,10]
    [TestCase("09:00:00", "09:11:59", 7)]  // [11,30]
    [TestCase("09:00:00", "09:30:59", 7)]  // [11,30]
    [TestCase("09:00:00", "09:31:59", 10)] // [31,59]
    [TestCase("09:00:00", "09:59:59", 10)] // [31,59]
    [TestCase("09:00:00", "10:00:59", 10)] // 整點
    [TestCase("09:00:00", "11:00:59", 20)] // 整點
    [TestCase("09:00:00", "12:00:59", 30)] // 整點
    [TestCase("09:00:00", "13:00:59", 40)] // 整點
    [TestCase("09:00:00", "14:00:59", 50)] // 整點
    [TestCase("09:00:00", "10:01:59", 17)] // 大於60分,1小時又剩餘<=30
    [TestCase("09:00:00", "10:30:59", 17)] // 大於60分,1小時又剩餘<=30
    [TestCase("09:00:00", "11:01:59", 27)] // 大於60分,2小時又剩餘<=30
    [TestCase("09:00:00", "11:30:59", 27)] // 大於60分,2小時又剩餘<=30
    [TestCase("09:00:00", "12:01:59", 37)] // 大於60分,3小時又剩餘<=30
    [TestCase("09:00:00", "12:30:59", 37)] // 大於60分,3小時又剩餘<=30
    [TestCase("09:00:00", "13:01:59", 47)] // 大於60分,4小時又剩餘<=30
    [TestCase("09:00:00", "13:30:59", 47)] // 大於60分,4小時又剩餘<=30
    [TestCase("09:00:00", "10:31:59", 20)] // 大於60分,1小時又剩餘 >30
    [TestCase("09:00:00", "10:59:59", 20)] // 大於60分,1小時又剩餘 >30
    [TestCase("09:00:00", "11:31:59", 30)] // 大於60分,2小時又剩餘 >30
    [TestCase("09:00:00", "11:59:59", 30)] // 大於60分,2小時又剩餘 >30
    [TestCase("09:00:00", "12:31:59", 40)] // 大於60分,3小時又剩餘 >30
    [TestCase("09:00:00", "12:59:59", 40)] // 大於60分,3小時又剩餘 >30
    [TestCase("09:00:00", "13:31:59", 50)] // 大於60分,4小時又剩餘 >30
    [TestCase("09:00:00", "13:59:59", 50)] // 大於60分,4小時又剩餘 >30
    [TestCase("09:00:00", "14:01:59", 50)] // 每天最多收50元
    [TestCase("00:00:00", "23:59:59", 50)] // 每天最多收50元
    public void CalcFee(string startValue, string endValue, int expected)
    {
        string date = "2022/5/6 ";
        startValue = date + startValue;
        endValue   = date + endValue;

        DateTime start = Convert.ToDateTime(startValue);
        DateTime end   = Convert.ToDateTime(endValue);

        var parkingMinutesCalculator = new ParkingMinutesCalculator();
        var actual                   = new ParkingDailyFeeTypeACalculator(parkingMinutesCalculator).Fee(start, end);

        Assert.AreEqual(expected, actual);
    }
}
