using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Repositories.Abstract;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IBookservice> _bookservice;
        private readonly Lazy<IAuthendicationService> _authendicationService;
        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
        {
            _bookservice = new Lazy<IBookservice>(() => new BookManager(repositoryManager,mapper));
            _authendicationService = new Lazy<IAuthendicationService>(() => new AuthendicationManager(mapper,userManager, configuration));   
        }
        public IBookservice Bookservice => _bookservice.Value;
        public IAuthendicationService AuthendicationService => _authendicationService.Value;
    }
}
