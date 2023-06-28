using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokBookStore.Areas.Manage.ViewModels;
using PustokBookStore.DAL;
using PustokBookStore.Entities;
using PustokBookStore.Helpers;

namespace PustokBookStore.Areas.Manage.Controllers
{
    [Area("manage")]
    public class BookController : Controller
    {
        private readonly PustokDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BookController(PustokDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var query = _context.Books.Include(x => x.Author).Include(x => x.Genre).Include(x => x.BookImages.Where(x => x.POsterStatus == true)).AsQueryable();
            return View(PaginatedList<Book>.Create(query, page, 4));
        }

        public IActionResult Create(int id)
        {
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.Tags = _context.Tags.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if(book.PosterFile == null)
            {
                ModelState.AddModelError("PosterFile", "PosterFile is required");
            }

            if (book.HoverPosterFile == null)
            {
                ModelState.AddModelError("HoverPosterFile", "HoverPosterFile is required");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Authors = _context.Authors.ToList();
                ViewBag.Genres = _context.Genres.ToList();
                ViewBag.Tags = _context.Tags.ToList();

                return View();
            }

            if(!_context.Authors.Any(x=>x.Id==book.AuthorId))
            {
                return View("Error");
            }

            if (!_context.Genres.Any(x => x.Id == book.GenreId))
            {
                return View("Error");
            }

            BookImage poster = new BookImage
            {
                POsterStatus = true,
                ImageName = FileManager.Save(book.PosterFile, _env.WebRootPath, "manage/uploads/books"),
            };
            _context.BookImages.Add(poster);

            BookImage hoverPoster = new BookImage
            {
                POsterStatus = false,
                ImageName = FileManager.Save(book.HoverPosterFile, _env.WebRootPath, "manage/uploads/books"),
            };
            book.BookImages.Add(hoverPoster);

            foreach (var file in book.ImageFiles)
            {
                BookImage bookImage = new BookImage
                {
                    POsterStatus = null,
                    ImageName = FileManager.Save(file, _env.WebRootPath, "manage/uploads/books"),
                };
                book.BookImages.Add(bookImage);
            }

            foreach (var tagId in book.TagIds)
            {
                if (!_context.Tags.Any(x => x.Id == tagId))
                {
                    return View("Error");
                }

                BookTag tag = new BookTag
                {
                    TagId = tagId
                };
                book.BookTags.Add(tag);
            }


            _context.Books.Add(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Book book = _context.Books.Include(x => x.BookImages).Include(x => x.BookTags).FirstOrDefault(x => x.Id == id);

            if (book == null) return View("Error");

            book.TagIds = book.BookTags.Select(x => x.TagId).ToList();

            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.Tags = _context.Tags.ToList();

            return View(book);
        }
    }
}
