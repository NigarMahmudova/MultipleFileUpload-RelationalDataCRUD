using Microsoft.AspNetCore.Mvc;
using PustokBookStore.Areas.Manage.ViewModels;
using PustokBookStore.DAL;
using PustokBookStore.Entities;
using PustokBookStore.Helpers;

namespace PustokBookStore.Areas.Manage.Controllers
{
    [Area("manage")]

    public class SliderController : Controller
    {
        private readonly PustokDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(PustokDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index(int page = 1)
        {
            var query = _context.Sliders.AsQueryable();
            return View(PaginatedList<Slider>.Create(query,page,2));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Slider slider, IFormFile imageFile)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (slider.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "ImageFile is required");
                return View();
            }

            
            slider.ImageName = FileManager.Save(slider.ImageFile, _env.WebRootPath, "/manage/uploads/sliders/");

            _context.Sliders.Add(slider);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Slider slider = _context.Sliders.Find(id);

            if (slider == null) return View("Error");

            return View(slider);
        }

        [HttpPost]
        public IActionResult Edit(Slider slider)
        {
            if (!ModelState.IsValid) return View();

            Slider existSlider = _context.Sliders.Find(slider.Id);

            if (existSlider == null) return View("Error");

            string removableImageName = null;

            if (slider.ImageFile != null)
            {
                removableImageName = slider.ImageName;
                existSlider.ImageName = FileManager.Save(slider.ImageFile, _env.WebRootPath, "manage/uploads/sliders");
            }

            _context.SaveChanges();

            existSlider.Title1 = slider.Title1;
            existSlider.Title2 = slider.Title2;
            existSlider.Order = slider.Order;
            existSlider.Desc = slider.Desc;
            existSlider.BtnText = slider.BtnText;
            existSlider.BtnUrl = slider.BtnUrl;

            if (removableImageName != null)
            {
                FileManager.Delete("manage/uploads/sliders", _env.WebRootPath, removableImageName);
            }

            return RedirectToAction("Index");
        }
    }
}
