using Entities.DTO;
using Entities.Models;
using Entities.RequestFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Abstract
{
    public interface IBookRepository:IRepositoryBase<Book>
    {
        PagedList<Book> GetAllBooks(BookParameters bookParameters, bool trackchanges);
        Book GetOneBookById(int id, bool trackchanges);
        void CreateOneBook(Book book);
        void UpdateOneBook(Book book);
        void DeleteOneBook(Book book);
    }
}
