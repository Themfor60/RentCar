using RentCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Data.Data.Repository.IRepository
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<VehicleInfo> Vehicles { get; }
        void SaveAsync();
    }
}
