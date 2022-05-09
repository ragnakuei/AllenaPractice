namespace ServicesLayer.ParkingFeeCalculators;

public interface IParkingDailyFeeCalculator
{
    int Fee(DateTime from, DateTime to);
}