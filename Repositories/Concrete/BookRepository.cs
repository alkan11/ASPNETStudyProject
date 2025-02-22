using Entities.Models;
using Repositories.Abstract;
using Repositories.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Concrete
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(ApplicationDBContext context) : base(context)
        {

        }

        public void CreateOneBook(Book book)
        {
            Create(book);
        }

        public void DeleteOneBook(Book book)
        {
            Delete(book);
        }

        public IQueryable<Book> GetAllBooks(bool trackchanges)
        {
            return FindAll(trackchanges);
        }

        public Book GetOneBookById(int id, bool trackchanges)
        {
            return FindByCondition(x => x.Id.Equals(id), trackchanges).SingleOrDefault();
        }

        public void UpdateOneBook(Book book)
        {
            Update(book);
        }
    }
}
