using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokBookStore.Areas.Manage.ViewModels;
using PustokBookStore.DAL;
using PustokBookStore.Entities;

namespace PustokBookStore.Areas.Manage.Controllers
{
    [Area("manage")]
    public class GenreController : Controller
    {
        private readonly PustokDbContext _context;
        public GenreController(PustokDbContext context)
        {
            _context= context;
        }
        public IActionResult Index(int page = 1, string search = null)
        {
            ViewBag.Search = search;

            var query = _context.Genres.Include(x=>x.Books).AsQueryable();

            if (search != null)
            {
                query = query.Where(x=>x.Name.Contains(search));
            }

            var model = PaginatedList<Genre>.Create(query, page, 2);

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Genre genre)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            if(_context.Genres.Any(x=> x.Name == genre.Name))
            {
                ModelState.AddModelError("Name", "Name is already taken.");
                return View();
            }
            _context.Genres.Add(genre);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Genre genre = _context.Genres.FirstOrDefault(x=>x.Id == id);

            if(genre == null)
            {
                return View("Error");
            }

            return View(genre);
        }

        [HttpPost]
        public IActionResult Edit(Genre genre)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            Genre existGenre = _context.Genres.FirstOrDefault(x => x.Id == genre.Id);

            if (genre.Name != existGenre.Name && _context.Genres.Any(x => x.Name == genre.Name))
            {
                ModelState.AddModelError("Name", "Name is already taken.");
                return View();
            }

            if(existGenre == null)
            {
                return View("Error");
            }

            existGenre.Name = genre.Name;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Genre genre = _context.Genres.Find(id);
            if(genre == null) return StatusCode(404);

            _context.Genres.Remove(genre);
            _context.SaveChanges();

            return StatusCode(200);
        }
    }
}
