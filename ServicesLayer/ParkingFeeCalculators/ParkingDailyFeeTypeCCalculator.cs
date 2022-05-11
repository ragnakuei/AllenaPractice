namespace ServicesLayer.ParkingFeeCalculators;

/// <summary>
/// 收費原則 C
/// 平日收費 :累進費率,第一小時為20元、第二小時起每小時30元, 平日每天最多收300元
/// 六日收費: 免費
/// </summary>
public class ParkingDailyFeeTypeCCalculator : IParkingDailyFeeCalculator
{
    private readonly IParkingMinutesCalculator _minutes01Calculator;

    public ParkingDailyFeeTypeCCalculator(IParkingMinutesCalculator minutes01Calculator)
    {
        _minutes01Calculator = minutes01Calculator;
    }

    public int Fee(DateTime from, DateTime to)
    {
        var minutes = _minutes01Calculator.Minutes(from, to);

        if (minutes == 0)
        {
            return 0;
        }

        var isHoliday = from.DayOfWeek == DayOfWeek.Saturday
                     || from.DayOfWeek == DayOfWeek.Sunday;

        return isHoliday
                   ? 0 // 六日免費
                   : FeeNormalDay(minutes);
    }

    /// <summary>
    /// 平日
    /// </summary>
    private int FeeNormalDay(int? minutes)
    {
        var hours = Math.Ceiling(minutes.Value / 60.0);

        var fee     = 0;
        var hourFee = 20;

        for (var i = 1; i <= hours; i++)
        {
            fee += hourFee;

            if (i == 1)
            {
                hourFee += 10;
            }
        }


        return Math.Min(300, fee);
    }
}
