using Entities.DTO;
using Entities.Models;
using Entities.RequestFeature;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.ActionFilter;
using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController:ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IServiceManager _serviceManager;

        public BooksController(IServiceManager serviceManager, ILogger<BooksController> logger)
        {
            _serviceManager = serviceManager;
            _logger = logger;
        }
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost]
        public IActionResult CreateOneBook(Book book)
        {
            _serviceManager.Bookservice.CreateOneBook(book);
            return Ok();
        }

        [HttpPut("id:int")]
        public IActionResult UpdateOneBook(int id, BookDto book)
        {
            _serviceManager.Bookservice.UpdateOneBook(id, book, false);

            return NoContent();
        }

        [HttpGet]
        public IActionResult GetAllBooks([FromQuery]BookParameters bookParameters)
        {
            var books = _serviceManager.Bookservice.GetAllBooks(bookParameters,false);
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOnebook(int id)
        {
            try
            {
                var book = _serviceManager.Bookservice.GetOnebookById(id, false);

                if (book is null)
                    return NotFound();

                return Ok(book);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("id:int")]
        public IActionResult DeleteOnebook(int id)
        {
            try
            {
                _serviceManager.Bookservice.DeleteOneBook(id, false);
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
