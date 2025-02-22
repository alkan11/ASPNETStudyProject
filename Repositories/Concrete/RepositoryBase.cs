using Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repositories.Concrete
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly ApplicationDBContext _context;

        public RepositoryBase(ApplicationDBContext context)
        {
            _context = context;
        }

        public  void Create(T entity)=> _context.Set<T>().Add(entity);


        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll(bool trackchanges)
        {
            if (!trackchanges)
            {
               return _context.Set<T>().AsNoTracking();
            }
            else
            {
               return _context.Set<T>(); 
            }
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackchanges)
        {
            if (!trackchanges)
            {
                return _context.Set<T>().Where(expression).AsNoTracking();
            }
            else
            {
                return _context.Set<T>().Where(expression);
            }
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
