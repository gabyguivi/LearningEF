using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challenge.Model;

namespace Challenge.Data.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        T GetById(object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> GetAll { get; }
    }
}
