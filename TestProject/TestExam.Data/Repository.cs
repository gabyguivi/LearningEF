using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestExam.Data.Interfaces;
using TestExam.Model;

namespace TestExam.Data
{

    public class Repository<T> : IRepository<T> where T : EntityInt
    {
        private readonly IContext _context;
        private IDbSet<T> _entities;

        public Repository(IContext context)
        {
            this._context = context;
        }

        public T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            this.Entities.Add(entity);
            this._context.SaveChanges();            
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            this._context.SaveChanges();            
        }

        public void Delete(T entity)
        {            
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            this.Entities.Remove(entity);
            this._context.SaveChanges();           
        }

        public virtual IQueryable<T> GetAll
        {
            get
            {
                return this.Entities;
            }
        }

        private IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = _context.Set<T>();
                }
                return _entities;
            }
        }
    }
}
