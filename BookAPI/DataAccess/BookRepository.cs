using BookAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookAPI.DataAccess
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _context;

        public BookRepository(BookContext context) => _context = context;

        public IEnumerable<Book> GetAllBooks() => _context.Books.ToList();

        public Book GetBookById(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                book.ViewsCount++;
                _context.SaveChanges();
            }
            return book;
        }

        public void AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }
    }

    public interface IBookRepository
    {
        IEnumerable<Book> GetAllBooks();
        Book GetBookById(int id);
        void AddBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(int id);
    }
}
