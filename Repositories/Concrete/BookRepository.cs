using Entities.Models;
using Entities.RequestFeature;
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

        public PagedList<Book> GetAllBooks(BookParameters bookParameters, bool trackchanges)
        {
            var aa = FindAll(trackchanges).ToList();

            return PagedList<Book>.ToPagedList(aa,bookParameters.PageNumber,bookParameters.PageSize);
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
