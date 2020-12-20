using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xurpas.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        T GetbyId(int Id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
