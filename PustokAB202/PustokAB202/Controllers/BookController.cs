using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokAB202.DAL;
using PustokAB202.Models;
using PustokAB202.ViewModels;

namespace PustokAB202.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDbContext _context;

        public BookController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Detail(int id)
        {
            Book book = _context.Books
                 .Include(x => x.Author)
                .Include(x => x.Genre)
                .Include(x => x.BookImages)
                .Include(x=>x.BookTags).ThenInclude(t=>t.Tag)
            .FirstOrDefault(x => x.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            List<Book> relatedBooks = _context.Books
                .Where(x=>x.GenreId==book.GenreId && x.Id!=book.Id)
                .Include(x=>x.Author)
                .Include(x=>x.BookImages)
                .ToList();

            DetailVM detailVM = new DetailVM
            {
                Book = book,
                RelatedBooks = relatedBooks,
            };

            return View(detailVM);
        }
    }
}
