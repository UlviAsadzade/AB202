using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaskHandTime.Models;

namespace TaskHandTime.Controllers
{
    public class HomeController : Controller
    {
        List<CustomService> Services = new List<CustomService>
        {
            new CustomService
            {
                Id= 1,
                Title="Said",
                Description="bla bla",
                Icon="said.png"
            },
            new CustomService
            {
                Id= 2,
                Title="Ali",
                Description="Mersiye",
                Icon="said.png"
            },
            new CustomService
            {
                Id= 3,
                Title="Fidan",
                Description="Deyingen",
                Icon="said.png"
            },
        };

        List<Product> Products = new List<Product>
        {
            new Product
            {
                Id= 1,
                Name="Iphone",
                Price=15
            },
            new Product
            {
                Id= 1,
                Name="Rolex",
                Price=15
            },
            new Product
            {
                Id= 1,
                Name="Swatch",
                Price=15
            },

        };

        public IActionResult Index()
        {
            ViewBag.Products = Products;
            return View(Services);
        }

    }
}