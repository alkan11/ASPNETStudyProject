using AutoMapper;
using Entities.DTO;
using Entities.Models;
using Entities.RequestFeature;
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
        private readonly IMapper _mapper;

        public BookManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
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

        public PagedList<Book> GetAllBooks(BookParameters bookParameters, bool trackchanges)
        {
            var books=_repositoryManager.Book.GetAllBooks(bookParameters,trackchanges);
            return books;
        } 

        public Book GetOnebookById(int id,bool trackchanges)
        {
            return _repositoryManager.Book.GetOneBookById(id, trackchanges);
        }

        public void UpdateOneBook(int id, BookDto book,bool trackchanges)
        {
            var entity = _repositoryManager.Book.GetOneBookById(id, trackchanges);
            if (entity is null)
            {
                new Exception($"book with id:{id} could not found");
            }

            if (book is null)
                throw new ArgumentNullException(nameof(book));
            //Mapping process
            //entity.Name=book.Name;
            //entity.Price=book.Price;

            entity=_mapper.Map<Book>(book);
            

            _repositoryManager.Book.Update(entity);
            _repositoryManager.Save();
        }
    }
}
