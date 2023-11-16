using Microsoft.AspNetCore.Mvc;
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

            HomeVM homeVM = new HomeVM
            {
                Sliders = Sliders,
                Features = Features,
            };
            return View(homeVM);
        }

       
    }
}