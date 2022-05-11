namespace ServicesLayer.ParkingFeeCalculators;

public class ParkingFeeCalculatorFactory
{
    public IParkingFeeCalculator Get(string parkingFeeCalculatorType, string parkingDailyFeeCalculatorType)
    {
        IParkingMinutesCalculator parkingMinutesCalculator = new ParkingMinutesCalculator();

        IParkingDailyFeeCalculator parkingDailyFeeCalculator
            = parkingDailyFeeCalculatorType switch
              {
                  nameof(ParkingDailyFeeTypeACalculator) => new ParkingDailyFeeTypeACalculator(parkingMinutesCalculator),
                  nameof(ParkingDailyFeeTypeBCalculator) => new ParkingDailyFeeTypeBCalculator(parkingMinutesCalculator),
                  nameof(ParkingDailyFeeTypeCCalculator) => new ParkingDailyFeeTypeCCalculator(parkingMinutesCalculator),
              };

        return parkingFeeCalculatorType switch
               {
                   nameof(ParkingFeeCalculatorV01) => new ParkingFeeCalculatorV01(parkingDailyFeeCalculator),
                   nameof(ParkingFeeCalculatorV02) => new ParkingFeeCalculatorV02(parkingDailyFeeCalculator),
               };
    }
}
