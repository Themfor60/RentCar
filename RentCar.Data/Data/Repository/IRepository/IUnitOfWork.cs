using RentCar.Models;
using System;
using System.Threading.Tasks;

namespace RentCar.Data.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<VehicleInfo> Vehicles { get; }
        Task SaveAsync();
    }
}
