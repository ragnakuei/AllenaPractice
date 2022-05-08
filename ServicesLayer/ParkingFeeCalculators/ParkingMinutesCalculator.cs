namespace ServicesLayer.ParkingFeeCalculators;

public class ParkingMinutesCalculator
{
    public int? Minutes(DateTime from, DateTime to)
    {
        if (from > to)
        {
            return null;
        }

        var fromWithSecondZero = from.AddSeconds(-from.Second);
        var toWithSecondZero   = to.AddSeconds(-to.Second);

        var minutes = (int)Math.Ceiling((toWithSecondZero - fromWithSecondZero).TotalMinutes);
        return minutes;
    }
}
