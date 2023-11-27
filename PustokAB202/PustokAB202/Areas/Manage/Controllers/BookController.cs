using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokAB202.Areas.Manage.ViewModels;
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

        public async Task<IActionResult> Update(int id)
        {
            Book book = await _context.Books.FirstOrDefaultAsync(x=>x.Id == id);

            if (book == null) return NotFound();

            BookUpdateVM updateVM = new BookUpdateVM
            {
                Name = book.Name,
                AuthorId = book.AuthorId,
                GenreId = book.GenreId,
                Page = book.Page,
                SalePrice = book.SalePrice,
                CostPrice = book.CostPrice,
                Discount = book.Discount,
                Desc = book.Desc,
                IsAvailable = book.IsAvailable,
                Authors = await _context.Authors.ToListAsync(),
                Genres = await _context.Genres.ToListAsync(),

            };

            return View(updateVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, BookUpdateVM updateVM)
        {
            if (!ModelState.IsValid)
            {
                updateVM.Authors = await _context.Authors.ToListAsync();
                updateVM.Genres = await _context.Genres.ToListAsync();
                return View(updateVM);
            }

            Book exist = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);

            if (exist == null) return NotFound();


            if (await _context.Authors.FirstOrDefaultAsync(x => x.Id == updateVM.AuthorId) is null)
            {
                ModelState.AddModelError("AuthorId", "This Author is not exist");
                updateVM.Authors = await _context.Authors.ToListAsync();
                updateVM.Genres = await _context.Genres.ToListAsync();
                return View(updateVM);
            }

            if (await _context.Genres.FirstOrDefaultAsync(x => x.Id == updateVM.GenreId) is null)
            {
                ModelState.AddModelError("GenreId", "This Genre is not exist");
                updateVM.Authors = await _context.Authors.ToListAsync();
                updateVM.Genres = await _context.Genres.ToListAsync();
                return View(updateVM);
            }

            exist.Name = updateVM.Name;
            exist.SalePrice = updateVM.SalePrice;
            exist.CostPrice = updateVM.CostPrice;
            exist.Discount= updateVM.Discount;
            exist.AuthorId = updateVM.AuthorId;
            exist.GenreId= updateVM.GenreId;
            exist.Page= updateVM.Page;
            exist.Desc= updateVM.Desc;
            exist.IsAvailable= updateVM.IsAvailable;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

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

        #region Detail
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
        #endregion
    }
}
