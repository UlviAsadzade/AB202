using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PustokAB202.DAL;
using PustokAB202.Models;
using PustokAB202.ViewModels;
using System.Security.Claims;

namespace PustokAB202.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public BookController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Detail(int id)
        {
            Book book = _context.Books
                 .Include(x => x.Author)
                .Include(x => x.Genre)
                .Include(x => x.BookImages)
                .Include(x => x.BookTags).ThenInclude(t => t.Tag)
            .FirstOrDefault(x => x.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            List<Book> relatedBooks = _context.Books
                .Where(x => x.GenreId == book.GenreId && x.Id != book.Id)
                .Include(x => x.Author)
                .Include(x => x.BookImages)
                .ToList();

            DetailVM detailVM = new DetailVM
            {
                Book = book,
                RelatedBooks = relatedBooks,
            };

            return View(detailVM);
        }

        public async Task<IActionResult> AddToBasket(int id)
        {
            if (id <= 0) return BadRequest();

            Book book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);

            if (book == null) return NotFound();
            List<BookItemVM> basket = new List<BookItemVM>();

            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.Users
                    .Include(x=>x.BasketItems)
                    .FirstOrDefaultAsync(x => x.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

                if(user == null) return NotFound();
                var basketItem = user.BasketItems.FirstOrDefault(x=>x.BookId == id);
                if(basketItem == null)
                {
                    user.BasketItems.Add(new BasketItem
                    {
                        BookId = book.Id,
                        Count = 1,
                    });
                }
                else
                {
                    basketItem.Count++;
                }

                await _context.SaveChangesAsync();


                foreach (var dbitem in user.BasketItems)
                {
                    Book newbook = await _context.Books
                        .Where(x => x.IsDeleted == false)
                        .Include(x => x.BookImages.Where(x => x.IsPrimary == true))
                        .FirstOrDefaultAsync(x => x.Id == dbitem.BookId);

                    if (newbook is not null)
                    {
                        basket.Add(new BookItemVM
                        {
                            Id = newbook.Id,
                            Name = newbook.Name,
                            Price = newbook.SalePrice - newbook.Discount,
                            Image = newbook.BookImages[0].Image,
                            Count = dbitem.Count,
                            SubTotal = (newbook.SalePrice - newbook.Discount) * dbitem.Count,

                        });
                    }
                }

            }
            else
            {
                string json = Request.Cookies["Books"];

                List<BookCookieVM> cookieVMs = new List<BookCookieVM>();
                BookCookieVM item = null;

                if (!string.IsNullOrEmpty(json))
                {
                    cookieVMs = JsonConvert.DeserializeObject<List<BookCookieVM>>(json);

                    item = cookieVMs.FirstOrDefault(x => x.Id == id);
                }

                if (item != null)
                {
                    item.Count++;
                }
                else
                {
                    BookCookieVM cookieVM = new BookCookieVM()
                    {
                        Id = id,
                        Count = 1
                    };

                    cookieVMs.Add(cookieVM);
                }

                Response.Cookies.Append("Books", JsonConvert.SerializeObject(cookieVMs));


                foreach (var cookie in cookieVMs)
                {
                    Book newbook = await _context.Books
                        .Where(x => x.IsDeleted == false)
                        .Include(x => x.BookImages.Where(x => x.IsPrimary == true))
                        .FirstOrDefaultAsync(x => x.Id == cookie.Id);

                    if (newbook is not null)
                    {
                        basket.Add(new BookItemVM
                        {
                            Id = newbook.Id,
                            Name = newbook.Name,
                            Price = newbook.SalePrice - newbook.Discount,
                            Image = newbook.BookImages[0].Image,
                            Count = cookie.Count,
                            SubTotal = (newbook.SalePrice - newbook.Discount) * cookie.Count,

                        });
                    }
                }
            }

            return PartialView("_Basketpartial", basket);
        }

        public async Task<IActionResult> GetBook(int id)
        {
            Book book = _context.Books
                 .Include(x => x.Author)
                .Include(x => x.Genre)
                .Include(x => x.BookImages)
                .Include(x => x.BookTags).ThenInclude(t => t.Tag)
            .FirstOrDefault(x => x.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            

            return PartialView("_BookModalPartial",book);
        }
    }
}
