using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokAB202.Areas.Manage.ViewModels;
using PustokAB202.Areas.Manage.ViewModels;
using PustokAB202.DAL;
using PustokAB202.Models;
using PustokAB202.Utilities.Extensions;

namespace PustokAB202.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BookController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BookController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            List<Book> Books = await _context.Books
                .Where(x=>x.IsDeleted==false)
                .Include(x => x.Author)
                .Include(x => x.Genre)
                .Include(x => x.BookImages)
                .Include(x=>x.BookTags).ThenInclude(x=>x.Tag)
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
            bookCreateVM.Tags = await _context.Tags.ToListAsync();

            return View(bookCreateVM);

        }

        [HttpPost]
        public async Task<IActionResult> Create(BookCreateVM bookCreateVM)
        {
            if (!ModelState.IsValid)
            {
                bookCreateVM.Authors = await _context.Authors.ToListAsync();
                bookCreateVM.Genres = await _context.Genres.ToListAsync();
                bookCreateVM.Tags = await _context.Tags.ToListAsync();

                return View(bookCreateVM);
            }
            if (bookCreateVM.AuthorId == 0)
            {
                ModelState.AddModelError("AuthorId", "You must choose Author");
                bookCreateVM.Authors = await _context.Authors.ToListAsync();
                bookCreateVM.Genres = await _context.Genres.ToListAsync();
                bookCreateVM.Tags = await _context.Tags.ToListAsync();

                return View(bookCreateVM);
            }

            if (bookCreateVM.GenreId == 0)
            {
                ModelState.AddModelError("GenreId", "You must choose Genre");
                bookCreateVM.Authors = await _context.Authors.ToListAsync();
                bookCreateVM.Genres = await _context.Genres.ToListAsync();
                bookCreateVM.Tags = await _context.Tags.ToListAsync();

                return View(bookCreateVM);
            }

            if (await _context.Authors.FirstOrDefaultAsync(x => x.Id == bookCreateVM.AuthorId) is null)
            {
                ModelState.AddModelError("AuthorId", "This Author is not exist");
                bookCreateVM.Authors = await _context.Authors.ToListAsync();
                bookCreateVM.Genres = await _context.Genres.ToListAsync();
                bookCreateVM.Tags = await _context.Tags.ToListAsync();

                return View(bookCreateVM);
            }

            if (await _context.Genres.FirstOrDefaultAsync(x => x.Id == bookCreateVM.GenreId) is null)
            {
                ModelState.AddModelError("GenreId", "This Genre is not exist");
                bookCreateVM.Authors = await _context.Authors.ToListAsync();
                bookCreateVM.Genres = await _context.Genres.ToListAsync();
                bookCreateVM.Tags = await _context.Tags.ToListAsync();

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
                IsAvailable = true,
                BookTags = new List<BookTag>(),
                BookImages = new List<BookImage>()
            };

            if(bookCreateVM.TagIds is not null)
            {
                foreach (var item in bookCreateVM.TagIds)
                {
                    if (!await _context.Tags.AnyAsync(x => x.Id == item))
                    {
                        ModelState.AddModelError("TagIds", "This tag is not exist");
                        bookCreateVM.Authors = await _context.Authors.ToListAsync();
                        bookCreateVM.Genres = await _context.Genres.ToListAsync();
                        bookCreateVM.Tags = await _context.Tags.ToListAsync();

                        return View(bookCreateVM);
                    }
                }

                foreach (var item in bookCreateVM.TagIds)
                {
                    book.BookTags.Add(new BookTag { TagId = item });
                }
            }

            if (!bookCreateVM.MainPhoto.CheckFileType("image/"))
            {
                ModelState.AddModelError("MainPhoto", "MainPhoto typei uygun deil");
                bookCreateVM.Authors = await _context.Authors.ToListAsync();
                bookCreateVM.Genres = await _context.Genres.ToListAsync();
                bookCreateVM.Tags = await _context.Tags.ToListAsync();

                return View(bookCreateVM);
            }
            if (bookCreateVM.MainPhoto.CheckFileLength(300))
            {
                ModelState.AddModelError("MainPhoto", "MainPhoto sizei uygun deil");
                bookCreateVM.Authors = await _context.Authors.ToListAsync();
                bookCreateVM.Genres = await _context.Genres.ToListAsync();
                bookCreateVM.Tags = await _context.Tags.ToListAsync();

                return View(bookCreateVM);
            }
            if (!bookCreateVM.HoverPhoto.CheckFileType("image/"))
            {
                ModelState.AddModelError("HoverPhoto", "HoverPhoto typei uygun deil");
                bookCreateVM.Authors = await _context.Authors.ToListAsync();
                bookCreateVM.Genres = await _context.Genres.ToListAsync();
                bookCreateVM.Tags = await _context.Tags.ToListAsync();

                return View(bookCreateVM);
            }
            if (bookCreateVM.HoverPhoto.CheckFileLength(300))
            {
                ModelState.AddModelError("HoverPhoto", "HoverPhoto sizei uygun deil");
                bookCreateVM.Authors = await _context.Authors.ToListAsync();
                bookCreateVM.Genres = await _context.Genres.ToListAsync();
                bookCreateVM.Tags = await _context.Tags.ToListAsync();

                return View(bookCreateVM);
            }

            if (!bookCreateVM.MainPhoto.CheckFileType("image/"))
            {
                ModelState.AddModelError("MainPhoto", "MainPhoto typei uygun deil");
                bookCreateVM.Authors = await _context.Authors.ToListAsync();
                bookCreateVM.Genres = await _context.Genres.ToListAsync();
                bookCreateVM.Tags = await _context.Tags.ToListAsync();

                return View(bookCreateVM);
            }
            if (bookCreateVM.MainPhoto.CheckFileLength(300))
            {
                ModelState.AddModelError("MainPhoto", "MainPhoto sizei uygun deil");
                bookCreateVM.Authors = await _context.Authors.ToListAsync();
                bookCreateVM.Genres = await _context.Genres.ToListAsync();
                bookCreateVM.Tags = await _context.Tags.ToListAsync();

                return View(bookCreateVM);
            }

            book.BookImages.Add(new BookImage
            {
                IsPrimary = true,
                Image = bookCreateVM.MainPhoto.CreateFile(_env.WebRootPath, "uploads/book")
            });
            book.BookImages.Add(new BookImage
            {
                IsPrimary = false,
                Image = bookCreateVM.HoverPhoto.CreateFile(_env.WebRootPath, "uploads/book")
            });
            foreach (var item in bookCreateVM.Photos ?? new List<IFormFile>() )
            {
                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photos", "Photos typei uygun deil");
                    bookCreateVM.Authors = await _context.Authors.ToListAsync();
                    bookCreateVM.Genres = await _context.Genres.ToListAsync();
                    bookCreateVM.Tags = await _context.Tags.ToListAsync();

                    return View(bookCreateVM);
                }
                if (item.CheckFileLength(300))
                {
                    ModelState.AddModelError("Photos", "Photos sizei uygun deil");
                    bookCreateVM.Authors = await _context.Authors.ToListAsync();
                    bookCreateVM.Genres = await _context.Genres.ToListAsync();
                    bookCreateVM.Tags = await _context.Tags.ToListAsync();

                    return View(bookCreateVM);
                }
                book.BookImages.Add(new BookImage
                {
                    IsPrimary = null,
                    Image = item.CreateFile(_env.WebRootPath, "uploads/book")
                });
            }
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int id)
        {
            Book book = await _context.Books
                .Include(x => x.BookTags)
                .Include(x => x.BookImages)

                .FirstOrDefaultAsync(x => x.Id == id);

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
                Images = book.BookImages,
                Authors = await _context.Authors.ToListAsync(),
                Genres = await _context.Genres.ToListAsync(),
                Tags = await _context.Tags.ToListAsync(),
                TagIds = book.BookTags.Select(x => x.TagId).ToList()

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
                updateVM.Tags = await _context.Tags.ToListAsync();
                return View(updateVM);
            }

            Book exist = await _context.Books
                .Include(x => x.BookTags)
                .ThenInclude(x => x.Tag)
                .Include(x => x.BookImages)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (exist == null) return NotFound();


            if (await _context.Authors.FirstOrDefaultAsync(x => x.Id == updateVM.AuthorId) is null)
            {
                ModelState.AddModelError("AuthorId", "This Author is not exist");
                updateVM.Authors = await _context.Authors.ToListAsync();
                updateVM.Genres = await _context.Genres.ToListAsync();
                updateVM.Tags = await _context.Tags.ToListAsync();

                return View(updateVM);
            }

            if (await _context.Genres.FirstOrDefaultAsync(x => x.Id == updateVM.GenreId) is null)
            {
                ModelState.AddModelError("GenreId", "This Genre is not exist");
                updateVM.Authors = await _context.Authors.ToListAsync();
                updateVM.Genres = await _context.Genres.ToListAsync();
                updateVM.Tags = await _context.Tags.ToListAsync();

                return View(updateVM);
            }

            if (updateVM.TagIds is not null)
            {
                exist.BookTags.RemoveAll(bt => !updateVM.TagIds.Exists(x => x == bt.TagId));

                var newList = updateVM.TagIds.Where(ti => !exist.BookTags.Any(x => x.TagId == ti)).ToList();

                foreach (var item in newList)
                {
                    exist.BookTags.Add(new BookTag { TagId = item });

                }

            }
            else
            {
                exist.BookTags = new List<BookTag>();
            }
            if (updateVM.MainPhoto is not null)
            {

                BookImage oldmain = exist.BookImages.FirstOrDefault(x => x.IsPrimary == true);
                oldmain.Image.DeleteFile(_env.WebRootPath, "uploads/book");
                exist.BookImages.Remove(oldmain);
                exist.BookImages.Add(new BookImage
                {
                    Image = updateVM.MainPhoto.CreateFile(_env.WebRootPath, "uploads/book"),
                    IsPrimary = true

                });
            }
            if (updateVM.HoverPhoto is not null)
            {

                BookImage oldhover = exist.BookImages.FirstOrDefault(x => x.IsPrimary == false);
                oldhover.Image.DeleteFile(_env.WebRootPath, "uploads/book");
                exist.BookImages.Remove(oldhover);
                exist.BookImages.Add(new BookImage
                {
                    Image = updateVM.HoverPhoto.CreateFile(_env.WebRootPath, "uploads/book"),
                    IsPrimary = false

                });
            }
            if (updateVM.ImageIds is null)
            {
                updateVM.ImageIds = new List<int>();
            }

            List<BookImage> removable = exist.BookImages.Where(pi => !updateVM.ImageIds.Exists(x => x == pi.Id) && pi.IsPrimary == null).ToList();
            foreach (BookImage photo in removable)
            {
                photo.Image.DeleteFile(_env.WebRootPath, "uploads/book");
                exist.BookImages.Remove(photo);
            }

            if (updateVM.Photos is not null)
            {
                foreach (IFormFile item in updateVM.Photos)
                {
                    if (!item.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photos", "Photos typei uygun deil");
                        updateVM.Authors = await _context.Authors.ToListAsync();
                        updateVM.Genres = await _context.Genres.ToListAsync();
                        updateVM.Tags = await _context.Tags.ToListAsync();

                        return View(updateVM);
                    }
                    if (item.CheckFileLength(300))
                    {
                        ModelState.AddModelError("Photos", "Photos sizei uygun deil");
                        updateVM.Authors = await _context.Authors.ToListAsync();
                        updateVM.Genres = await _context.Genres.ToListAsync();
                        updateVM.Tags = await _context.Tags.ToListAsync();

                        return View(updateVM);
                    }

                    exist.BookImages.Add(new BookImage
                    {
                        Image = item.CreateFile(_env.WebRootPath, "uploads/book"),
                        IsPrimary = null
                    });
                }

            }



            exist.Name = updateVM.Name;
            exist.SalePrice = updateVM.SalePrice;
            exist.CostPrice = updateVM.CostPrice;
            exist.Discount = updateVM.Discount;
            exist.AuthorId = updateVM.AuthorId;
            exist.GenreId = updateVM.GenreId;
            exist.Page = updateVM.Page;
            exist.Desc = updateVM.Desc;
            exist.IsAvailable = updateVM.IsAvailable;


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
