namespace ServicesLayer.ParkingFeeCalculators.Models;

public class ParkingFee
{
    public IList<SingleDayFee> Items { get; init; }

    public int TotalFee { get; init; }
}
