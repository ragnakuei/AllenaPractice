using ServicesLayer.ParkingFeeCalculators.Models;

namespace ServicesLayer.ParkingFeeCalculators;

public interface IParkingFeeCalculator
{
    ParkingFee CalcParkingFee(DateTime from, DateTime to);
}
