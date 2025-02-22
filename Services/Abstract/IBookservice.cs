using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IBookservice
    {
        IEnumerable<Book> GetAllBooks(bool trackchanges);
        Book GetOnebookById(int id, bool trackChanges);
        Book CreateOneBook(Book book);
        void UpdateOneBook(int id, Book book, bool trackChanges);
        void DeleteOneBook(int id, bool trackChanges);
    }
}
