using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokAB202.DAL;
using PustokAB202.Models;
using PustokAB202.ViewModels;
using System.Diagnostics;

namespace PustokAB202.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        

        public IActionResult Index()
        {
            List<Slider> Sliders = _context.Sliders.OrderBy(x=>x.Order).ToList();
            List<Feature> Features = _context.Features.ToList();
            List<Book> Books = _context.Books
                .Include(x=>x.Author)
                .Include(x=>x.Genre)
                .Include(x=>x.BookImages)
                .ToList();

			HomeVM homeVM = new HomeVM
            {
                Sliders = Sliders,
                Features = Features,
                Books = Books,
			};
            return View(homeVM);
        }

        public IActionResult Details(int id)
        {
            Book book = _context.Books
                 .Include(x => x.Author)
                .Include(x => x.Genre)
                .Include(x => x.BookImages).
            FirstOrDefault(x => x.Id == id);

            if(book == null)
            {
                return NotFound();
            }

            return View(book);
        }

       
    }
}