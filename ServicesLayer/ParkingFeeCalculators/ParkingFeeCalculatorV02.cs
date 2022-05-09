using ServicesLayer.ParkingFeeCalculators.Models;

namespace ServicesLayer.ParkingFeeCalculators;

public class ParkingFeeCalculatorV02
{
    private readonly IParkingDailyFeeCalculator _parkingDailyFeeCalculator;

    public ParkingFeeCalculatorV02(IParkingDailyFeeCalculator parkingDailyFeeCalculator)
    {
        _parkingDailyFeeCalculator = parkingDailyFeeCalculator;
    }

    public ParkingFee CalcParkingFee(DateTime from, DateTime to)
    {
        var fromDate = DateOnly.FromDateTime(from);
        var toDate   = DateOnly.FromDateTime(to);

        var items = new List<SingleDayFee>();

        var dayOfStartTime = new TimeOnly(0,  0,  0);
        var dayOfEndTime   = new TimeOnly(23, 59, 0);

        var dayFrom = from;
        var dayTo   = fromDate.ToDateTime(dayOfEndTime);

        bool isBreak = false;
        
        do
        {
            if(DateOnly.FromDateTime(dayTo) == toDate)
            {
                dayTo = to;
                isBreak = true;
            }

            items.Add(new SingleDayFee
                      {
                          StartTime = dayFrom,
                          EndTime   = dayTo,
                          Fee       = _parkingDailyFeeCalculator.Fee(dayFrom, dayTo),
                      });
            
            if (isBreak)
            {
                break;
            }
            
            var nextDay = DateOnly.FromDateTime(dayFrom).AddDays(1);
            dayFrom = nextDay.ToDateTime(dayOfStartTime);
            dayTo   = nextDay.ToDateTime(dayOfEndTime);
        } while (true);

        var result = new ParkingFee
                     {
                         Items    = items,
                         TotalFee = items.Sum(i => i.Fee)
                     };
        return result;
    }
}
