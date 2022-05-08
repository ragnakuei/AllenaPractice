namespace ServicesLayer.ParkingFeeCalculators;

public class ParkingDailyFeeCalculator
{
    private readonly ParkingMinutesCalculator _minutesCalculator;

    public ParkingDailyFeeCalculator(ParkingMinutesCalculator minutesCalculator)
    {
        _minutesCalculator = minutesCalculator;
    }

    /// <summary>
    /// 當天最高收費金額
    /// </summary>
    public int MaxFee => 50;

    public int Fee(DateTime from, DateTime to)
    {
        var minutes = _minutesCalculator.Minutes(from, to);
        var fee = minutes switch
                  {
                      <= 10  => 0,
                      <= 30  => 7,  // 可註解，但為了符合需求，故保留
                      <= 59  => 10, // 可註解，但為了符合需求，故保留
                      <= 300 => CalculateFeeOverOneHour(minutes.GetValueOrDefault()),
                      _      => MaxFee,
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
