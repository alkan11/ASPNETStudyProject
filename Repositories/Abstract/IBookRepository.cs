﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Abstract
{
    public interface IBookRepository:IRepositoryBase<Book>
    {
        IQueryable<Book> GetAllBooks(bool trackchanges);
        Book GetOneBookById(int id, bool trackchanges);
        void CreateOneBook(Book book);
        void UpdateOneBook(Book book);
        void DeleteOneBook(Book book);
    }
}
