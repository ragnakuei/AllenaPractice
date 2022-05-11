namespace ServicesLayer.ParkingFeeCalculators;

/// <summary>
/// 收費原則 B
/// 平日：
/// 未滿 10 分鐘：免費
/// 滿 10 分鐘，至半小時 7 元
/// 超過半小時，至一小時 10 元
/// 滿一小時後，前半小時 7 元，後半小時 10 元
/// 每天至多收費 50 元
/// 六日：
/// 滿 1 分鐘以每小時計
/// 每小時 15 元
/// 每天至多收費 250 元
/// </summary>
public class ParkingDailyFeeTypeBCalculator : IParkingDailyFeeCalculator
{
    private readonly IParkingMinutesCalculator _minutes01Calculator;

    public ParkingDailyFeeTypeBCalculator(IParkingMinutesCalculator minutes01Calculator)
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

        var isWeekend = from.DayOfWeek == DayOfWeek.Saturday
                     || from.DayOfWeek == DayOfWeek.Sunday;

        return isWeekend
                   ? FeeWeekend(minutes)
                   : FeeNormalDay(minutes);
    }

    /// <summary>
    /// 六日
    /// </summary>
    private int FeeWeekend(int? minutes)
    {
        var paidHours = Math.Ceiling(minutes.Value / 60.0);

        var fee = (int)Math.Min(paidHours * 15, 250);

        return fee;
    }

    /// <summary>
    /// 平日
    /// </summary>
    private int FeeNormalDay(int? minutes)
    {
        var fee = minutes switch
                  {
                      <= 10  => 0,
                      <= 30  => 7,  // 可註解，但為了符合需求，故保留
                      <= 59  => 10, // 可註解，但為了符合需求，故保留
                      <= 300 => FeeOverOneHourNormalDay(minutes.GetValueOrDefault()),
                      _      => 50,
                  };
        return fee;
    }

    /// <summary>
    /// 平日 超過一小時
    /// </summary>
    private int FeeOverOneHourNormalDay(int totalMinutes)
    {
        var timeSpan = new TimeSpan(0, totalMinutes, 0);

        var fee = timeSpan.Hours * 10;
        if (timeSpan.Minutes > 0)
        {
            fee += timeSpan.Minutes <= 30
                       ? 7   // 滿1小時內，1 分鐘至 30 分鐘，7 元
                       : 10; // 滿1小時內，31 分鐘至5 9 分鐘，10 元
        }

        return fee;
    }
}
