using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokAB202.DAL;
using PustokAB202.Models;

namespace PustokAB202.ViewComponents
{
    public class BookForTagViewComponent : ViewComponent
    {
        readonly AppDbContext _context;

        public BookForTagViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string tagname)
        {
            List<Book> books = await _context.Books
                .Where(x=>x.IsDeleted==false && x.BookTags.Any(y=>y.Tag.Name==tagname))
                .ToListAsync();

            return View(books);
        }
    }
}
