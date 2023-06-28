using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokBookStore.Areas.Manage.ViewModels;
using PustokBookStore.DAL;
using PustokBookStore.Entities;

namespace PustokBookStore.Areas.Manage.Controllers
{
    [Area("manage")]
    public class AuthorController : Controller
    {
        private readonly PustokDbContext _context;
        public AuthorController(PustokDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            var query = _context.Authors.Include(x => x.Books);

            var model = PaginatedList<Author>.Create(query, page, 2);

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Author author)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            if (_context.Authors.Any(x => x.FullName == author.FullName))
            {
                ModelState.AddModelError("FullName", "FullName is already taken.");
                return View();
            }

            _context.Authors.Add(author);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Author author = _context.Authors.FirstOrDefault(x => x.Id == id);

            if (author == null)
            {
                return View("Error");
            }

            return View(author);
        }

        [HttpPost]
        public IActionResult Edit(Author author)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Author existAuthor = _context.Authors.FirstOrDefault(x =>x.Id == author.Id);

            if (existAuthor == null)
            {
                return View("Error");
            }
            existAuthor.FullName = author.FullName;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            Author author = _context.Authors.Include(x => x.Books).FirstOrDefault(x => x.Id == id);
            if (author == null) return View("Error");
            return View(author);
        }

        [HttpPost]
        public IActionResult Delete(Author author)
        {
            Author existAuthor = _context.Authors.Find(author.Id);
            if (existAuthor == null) return View("Error");

            _context.Authors.Remove(existAuthor);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
