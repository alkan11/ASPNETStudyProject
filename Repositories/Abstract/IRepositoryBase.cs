﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Abstract
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool trackchanges);
        IQueryable<T> FindByCondition(Expression<Func<T,bool>> expression,bool trackchanges);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
