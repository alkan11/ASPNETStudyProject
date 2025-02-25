using Entities.DTO;
using Entities.Models;
using Entities.RequestFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IBookservice
    {
       PagedList<Book> GetAllBooks(BookParameters bookParameters, bool trackchanges);
        Book GetOnebookById(int id, bool trackChanges);
        Book CreateOneBook(Book book);
        void UpdateOneBook(int id, BookDto book, bool trackChanges);
        void DeleteOneBook(int id, bool trackChanges);
    }
}
