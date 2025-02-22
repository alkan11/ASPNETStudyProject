using Entities.Models;
using Repositories.Abstract;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookManager : IBookservice
    {
        private readonly IRepositoryManager _repositoryManager;

        public BookManager(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public Book CreateOneBook(Book book)
        {
            _repositoryManager.Book.CreateOneBook(book);
            _repositoryManager.Save();
            return book;
        }

        public void DeleteOneBook(int id, bool trackChanges)
        {
            var entity = _repositoryManager.Book.GetOneBookById(id, trackChanges);
            if (entity is null)
            {
                new Exception($"book with id:{id} could not found");
            }

            _repositoryManager.Book.DeleteOneBook(entity);
            _repositoryManager.Save();
        }

        public IEnumerable<Book> GetAllBooks(bool trackchanges)
        {
            return _repositoryManager.Book.GetAllBooks(trackchanges);
        }

        public Book GetOnebookById(int id,bool trackchanges)
        {
            return _repositoryManager.Book.GetOneBookById(id, trackchanges);
        }

        public void UpdateOneBook(int id, Book book,bool trackchanges)
        {
            var entity = _repositoryManager.Book.GetOneBookById(id, trackchanges);
            if (entity is null)
            {
                new Exception($"book with id:{id} could not found");
            }

            if (book is null)
                throw new ArgumentNullException(nameof(book));

            entity.Name=book.Name;
            entity.Price=book.Price;

            _repositoryManager.Book.UpdateOneBook(entity);
            _repositoryManager.Save();
        }
    }
}
