namespace ServicesLayer.ParkingFeeCalculators;

public class ParkingFeeCalculator
{
    private readonly ParkingDailyFeeCalculator _parkingDailyFeeCalculator;

    public ParkingFeeCalculator(ParkingDailyFeeCalculator parkingDailyFeeCalculator)
    {
        _parkingDailyFeeCalculator = parkingDailyFeeCalculator;
    }

    public int Fee(DateTime from, DateTime to)
    {
        var fromDate = DateOnly.FromDateTime(from);
        var toDate   = DateOnly.FromDateTime(to);

        var crossDays = fromDate.Days(toDate);

        var fee = 0;

        if (crossDays >= 1)
        {
            var dayOfEndTime   = new TimeOnly(23, 59, 0);
            // First Day
            var firstDayFrom = from;
            var firstDayTo   = fromDate.ToDateTime(dayOfEndTime);
            fee += _parkingDailyFeeCalculator.Fee(firstDayFrom, firstDayTo);

            var dayOfStartTime = new TimeOnly(0,  0,  0);
            // Last Day
            var lastDayFrom = toDate.ToDateTime(dayOfStartTime);
            var lastDayTo   = to;
            fee += _parkingDailyFeeCalculator.Fee(lastDayFrom, lastDayTo);

            // Middle Days
            var middleDays = crossDays - 1;
            if (middleDays > 0)
            {
                fee += _parkingDailyFeeCalculator.MaxFee * middleDays;
            }
        }
        else
        {
            fee += _parkingDailyFeeCalculator.Fee(from, to);
        }

        return fee;
    }
}
