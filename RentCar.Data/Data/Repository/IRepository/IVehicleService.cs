using RentCar.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IVehicleService
{
    Task<List<VehicleInfo>> GetVehicleData(string make, string model);
}
