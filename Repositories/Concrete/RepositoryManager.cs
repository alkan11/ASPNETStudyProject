using Repositories.Abstract;
using Repositories.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Concrete
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDBContext _context;
        private readonly Lazy<IBookRepository> _bookRepository;

        public RepositoryManager(ApplicationDBContext context)
        {
            _context = context;
            _bookRepository=new Lazy<IBookRepository>(()=>new BookRepository(_context));
        }

        public IBookRepository Book => _bookRepository.Value;

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
