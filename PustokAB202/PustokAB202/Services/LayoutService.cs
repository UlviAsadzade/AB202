using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PustokAB202.DAL;
using PustokAB202.Models;
using PustokAB202.ViewModels;
using System.Security.Claims;

namespace PustokAB202.Services
{
    public class LayoutService
    {
        readonly AppDbContext _context;
        readonly IHttpContextAccessor _accessor;
        private readonly UserManager<AppUser> _userManager;


        public LayoutService(AppDbContext context, IHttpContextAccessor accessor, UserManager<AppUser> userManager)
        {
            _context = context;
            _accessor = accessor;
            _userManager = userManager;
        }

        public async Task<List<BookItemVM>> GetBasketAsync()
        {
           string json =  _accessor.HttpContext.Request.Cookies["Books"];

            List<BookItemVM> basket = new List<BookItemVM>();

            if(_accessor.HttpContext.User.Identity.IsAuthenticated)
            {

                AppUser user = await _userManager.Users
                    .Include(x => x.BasketItems)
                    .ThenInclude(x=>x.Book)
                    .ThenInclude(y=>y.BookImages.Where(x=>x.IsPrimary==true))
                    .FirstOrDefaultAsync(x => x.Id == _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

                foreach (var item in user.BasketItems)
                {
                    basket.Add(new BookItemVM
                    {
                        Id = item.Book.Id,
                        Name = item.Book.Name,
                        Price = item.Book.SalePrice - item.Book.Discount,
                        Image = item.Book.BookImages[0].Image,
                        Count = item.Count,
                        SubTotal = (item.Book.SalePrice - item.Book.Discount) * item.Count,
                    });
                }


            }
            else
            {
                if (json != null)
                {
                    List<BookCookieVM> cookies = JsonConvert.DeserializeObject<List<BookCookieVM>>(json);

                    foreach (var item in cookies)
                    {
                        Book book = await _context.Books
                            .Where(x => x.IsDeleted == false)
                            .Include(x => x.BookImages.Where(x => x.IsPrimary == true))
                            .FirstOrDefaultAsync(x => x.Id == item.Id);

                        if (book is not null)
                        {
                            basket.Add(new BookItemVM
                            {
                                Id = book.Id,
                                Name = book.Name,
                                Price = book.SalePrice - book.Discount,
                                Image = book.BookImages[0].Image,
                                Count = item.Count,
                                SubTotal = (book.SalePrice - book.Discount) * item.Count,

                            });
                        }
                    }

                }
            }

            

            return basket;
        }
    }
}
