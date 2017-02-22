using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestExam.Model;

namespace TestExam.Data.Interfaces
{
    public interface IRepository<T> where T : EntityInt
    {
        T GetById(object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> GetAll { get; }
    }
}
