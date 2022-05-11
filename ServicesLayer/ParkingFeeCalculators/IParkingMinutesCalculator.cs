namespace ServicesLayer.ParkingFeeCalculators;

public interface IParkingMinutesCalculator
{
    int? Minutes(DateTime from, DateTime to);
}
