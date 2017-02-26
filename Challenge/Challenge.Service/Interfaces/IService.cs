using System.Collections.Generic;
using Challenge.Model;

namespace Challenge.Service.Interfaces
{

    public interface IService<T> where T : EntityInt
    {
        void Insert(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
        void Update(T entity);
    }
}
