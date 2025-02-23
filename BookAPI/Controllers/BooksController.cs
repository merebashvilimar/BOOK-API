using BookAPI.DataAccess;
using BookAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository) => _bookRepository = bookRepository;

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBooks() => Ok(_bookRepository.GetAllBooks());

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = _bookRepository.GetBookById(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        public ActionResult<Book> CreateBook(Book book)
        {
            _bookRepository.AddBook(book);
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book book)
        {
            if (id != book.Id) return BadRequest();
            _bookRepository.UpdateBook(book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            _bookRepository.DeleteBook(id);
            return NoContent();
        }
    }
}
