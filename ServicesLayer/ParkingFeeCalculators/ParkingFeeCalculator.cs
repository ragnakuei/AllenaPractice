using ServicesLayer.ParkingFeeCalculators.Models;

namespace ServicesLayer.ParkingFeeCalculators;

public class ParkingFeeCalculator
{
    private readonly ParkingDailyFeeCalculator _parkingDailyFeeCalculator;

    public ParkingFeeCalculator(ParkingDailyFeeCalculator parkingDailyFeeCalculator)
    {
        _parkingDailyFeeCalculator = parkingDailyFeeCalculator;
    }

    public ParkingFee CalcParkingFee(DateTime from, DateTime to)
    {
        var fromDate = DateOnly.FromDateTime(from);
        var toDate   = DateOnly.FromDateTime(to);

        var crossDays = fromDate.Days(toDate);

        var items = new List<SingleDayFee>();

        if (crossDays >= 1)
        {
            var dayOfStartTime = new TimeOnly(0,  0,  0);
            var dayOfEndTime   = new TimeOnly(23, 59, 0);

            // First Day
            var firstDayFrom = from;
            var firstDayTo   = fromDate.ToDateTime(dayOfEndTime);
            items.Add(new SingleDayFee
                      {
                          StartTime = firstDayFrom,
                          EndTime   = firstDayTo,
                          Fee       = _parkingDailyFeeCalculator.Fee(firstDayFrom, firstDayTo),
                      });

            // Middle Days
            var middleDays = crossDays - 1;
            for (int i = 0; i < middleDays; i++)
            {
                var middleDay     = fromDate.AddDays(i + 1);
                var middleDayFrom = middleDay.ToDateTime(dayOfStartTime);
                var middleDayTo   = middleDay.ToDateTime(dayOfEndTime);

                items.Add(new SingleDayFee
                          {
                              StartTime = middleDayFrom,
                              EndTime   = middleDayTo,
                              Fee       = _parkingDailyFeeCalculator.Fee(middleDayFrom, middleDayTo),
                          });
            }

            // Last Day
            var lastDayFrom = toDate.ToDateTime(dayOfStartTime);
            var lastDayTo   = to;
            items.Add(new SingleDayFee
                      {
                          StartTime = lastDayFrom,
                          EndTime   = lastDayTo,
                          Fee       = _parkingDailyFeeCalculator.Fee(lastDayFrom, lastDayTo),
                      });
        }
        else
        {
            items.Add(new SingleDayFee
                      {
                          StartTime = from,
                          EndTime   = to,
                          Fee       = _parkingDailyFeeCalculator.Fee(from, to),
                      });
        }

        var result = new ParkingFee
                     {
                         Items    = items,
                         TotalFee = items.Sum(i => i.Fee)
                     };
        return result;
    }
}
