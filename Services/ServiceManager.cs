using Repositories.Abstract;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IBookservice> _bookservice;
        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _bookservice = new Lazy<IBookservice>(() => new BookManager(repositoryManager));   
        }
        public IBookservice Bookservice => _bookservice.Value;
    }
}
