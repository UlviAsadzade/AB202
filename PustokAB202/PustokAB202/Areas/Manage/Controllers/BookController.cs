using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokAB202.Areas.Manage.ViewModels;
using PustokAB202.DAL;
using PustokAB202.Models;

namespace PustokAB202.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BookController : Controller
    {
        private readonly AppDbContext _context;

        public BookController(AppDbContext context)
        {
            _context = context;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            List<Book> Books = await _context.Books
                .Where(x=>x.IsDeleted==false)
                .Include(x => x.Author)
                .Include(x => x.Genre)
                .Include(x => x.BookImages)
                .ToListAsync();

            return View(Books);
        } 
        #endregion

        #region Create
        public async Task<IActionResult> Create()
        {
            BookCreateVM bookCreateVM = new BookCreateVM();

            bookCreateVM.Authors = await _context.Authors.ToListAsync();
            bookCreateVM.Genres = await _context.Genres.ToListAsync();

            return View(bookCreateVM);

        }

        [HttpPost]
        public async Task<IActionResult> Create(BookCreateVM bookCreateVM)
        {
            if (!ModelState.IsValid)
            {
                bookCreateVM.Authors = await _context.Authors.ToListAsync();
                bookCreateVM.Genres = await _context.Genres.ToListAsync();
                return View(bookCreateVM);
            }
            if (bookCreateVM.AuthorId == 0)
            {
                ModelState.AddModelError("AuthorId", "You must choose Author");
                bookCreateVM.Authors = await _context.Authors.ToListAsync();
                bookCreateVM.Genres = await _context.Genres.ToListAsync();
                return View(bookCreateVM);
            }

            if (bookCreateVM.GenreId == 0)
            {
                ModelState.AddModelError("GenreId", "You must choose Genre");
                bookCreateVM.Authors = await _context.Authors.ToListAsync();
                bookCreateVM.Genres = await _context.Genres.ToListAsync();
                return View(bookCreateVM);
            }

            if (await _context.Authors.FirstOrDefaultAsync(x => x.Id == bookCreateVM.AuthorId) is null)
            {
                ModelState.AddModelError("AuthorId", "This Author is not exist");
                bookCreateVM.Authors = await _context.Authors.ToListAsync();
                bookCreateVM.Genres = await _context.Genres.ToListAsync();
                return View(bookCreateVM);
            }

            if (await _context.Genres.FirstOrDefaultAsync(x => x.Id == bookCreateVM.GenreId) is null)
            {
                ModelState.AddModelError("GenreId", "This Genre is not exist");
                bookCreateVM.Authors = await _context.Authors.ToListAsync();
                bookCreateVM.Genres = await _context.Genres.ToListAsync();
                return View(bookCreateVM);
            }



            Book book = new Book()
            {
                Name = bookCreateVM.Name,
                AuthorId = bookCreateVM.AuthorId,
                GenreId = bookCreateVM.GenreId,
                Page = bookCreateVM.Page,
                SalePrice = bookCreateVM.SalePrice,
                CostPrice = bookCreateVM.CostPrice,
                Discount = bookCreateVM.Discount,
                Desc = bookCreateVM.Desc,
                IsDeleted = false,
                IsAvailable = true
            };

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            Book book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);

            if (book == null) return NotFound();

            book.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        } 
        #endregion

        public async Task<IActionResult> Detail(int id)
        {
            Book book = await _context.Books
                .Include(x => x.Author)
                .Include(x => x.Genre)
                .Include(x => x.BookImages)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (book == null) return NotFound();

            return View(book);
        }
    }
}
