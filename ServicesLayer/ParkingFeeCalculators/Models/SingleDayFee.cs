namespace ServicesLayer.ParkingFeeCalculators.Models;

public class SingleDayFee
{
    /// <summary>
    /// 精確到分鐘的入場時間
    /// </summary>
    public DateTime StartTime { get; init; }

    /// <summary>
    /// 精確到分鐘的離場時間
    /// </summary>
    public DateTime EndTime { get; init; }

    /// <summary>
    /// 本日應收取費用
    /// </summary>
    public int Fee { get; init; }
}
