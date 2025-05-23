using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Data.Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task <T> Get(int id);
        Task <IEnumerable<T>> GetAll();
        void Add(T Entity);
        void Update(T Entity);
        void Delete(int id);
        
    }
}
