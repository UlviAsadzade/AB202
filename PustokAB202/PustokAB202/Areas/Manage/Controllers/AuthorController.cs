using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokAB202.DAL;
using PustokAB202.Models;

namespace PustokAB202.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AuthorController : Controller
    {
        private readonly AppDbContext _context;

        public AuthorController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Author> authors = await _context.Authors.Include(x=>x.Books).ToListAsync();
            return View(authors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Author author)
        {
            bool result =await _context.Authors.AnyAsync(x=>x.Fullname== author.Fullname);

            if(!ModelState.IsValid)
            {
                return View();
            }

            if (result)
            {
                ModelState.AddModelError("Fullname", "Eyni adli yazici yarana bilmez");
                return View();
            }

            await _context.AddAsync(author);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            Author author =await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);

            if (author == null) return NotFound();

            return View(author);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id,Author author)
        {
            Author exist = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);

            if (exist == null) return NotFound();

            bool result = await _context.Authors.AnyAsync(x => x.Fullname == author.Fullname);

            if (!ModelState.IsValid)
            {
                return View(exist);
            }

            if (result)
            {
                ModelState.AddModelError("Fullname", "Eyni adli yazici yarana bilmez");
                return View(exist);
            }

            exist.Fullname= author.Fullname;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            Author exist = await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);

            if (exist == null) return NotFound();

            _context.Authors.Remove(exist);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
