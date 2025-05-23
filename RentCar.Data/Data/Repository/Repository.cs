using Microsoft.EntityFrameworkCore;
using RentCar.Data.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Data.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        // aqui estamos haciendo una Inyección del contexto de la base de datos (ApplicationDbContext)
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;


        //este es el constructor, para inicializar el dbset 
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }


        //Agregar datos
        public void Add(T Entity)
        {
            _dbSet.Add(Entity);
        }

        //Metodo de eliminar
        public async Task Delete(int id)
        {
            var entity = await Get(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        //Metodo de obtener un dato por su id 
        public async Task <T> Get(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        //Metodo de tener todos los datos de la tabla
        public async Task <IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public void Update(T Entity)
        {
            _dbSet.Update(Entity);
        }

        void IRepository<T>.Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
