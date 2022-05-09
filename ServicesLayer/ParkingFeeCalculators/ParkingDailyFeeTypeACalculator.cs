namespace ServicesLayer.ParkingFeeCalculators;

/// <summary>
/// 收費原則 A
/// 未滿 10 分鐘：免費
/// 滿 10 分鐘，至半小時 7 元
/// 超過半小時，至一小時 10 元
/// 滿一小時後，前半小時 7 元，後半小時 10 元
/// 每天至多收費 50 元
/// </summary>
public class ParkingDailyFeeTypeACalculator : IParkingDailyFeeCalculator
{
    private readonly ParkingMinutesCalculator _minutesCalculator;

    public ParkingDailyFeeTypeACalculator(ParkingMinutesCalculator minutesCalculator)
    {
        _minutesCalculator = minutesCalculator;
    }

    public int Fee(DateTime from, DateTime to)
    {
        var minutes = _minutesCalculator.Minutes(from, to);
        var fee = minutes switch
                  {
                      <= 10  => 0,
                      <= 30  => 7,  // 可註解，但為了符合需求，故保留
                      <= 59  => 10, // 可註解，但為了符合需求，故保留
                      <= 300 => CalculateFeeOverOneHour(minutes.GetValueOrDefault()),
                      _      => 50, // 當天最高收費金額
                  };
        return fee;
    }

    private int CalculateFeeOverOneHour(int totalMinutes)
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
