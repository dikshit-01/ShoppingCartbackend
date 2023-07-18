using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        T FindById(object id);

        void Save();
    }
}
