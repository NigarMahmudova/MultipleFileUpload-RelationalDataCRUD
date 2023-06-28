using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PustokBookStore.DAL;
using PustokBookStore.Entities;
using PustokBookStore.ViewModels;

namespace PustokBookStore.Controllers
{
    public class BookController : Controller
    {
        readonly PustokDbContext _context;
        public BookController(PustokDbContext context)
        {
            _context = context;
        }
        public IActionResult GetDetail(int id)
        {
            Book book = _context.Books
                .Include(x => x.BookImages)
                .Include(x => x.Genre)
                .Include(x => x.Author)
                .Include(x => x.BookTags).ThenInclude(x =>x.Tag)
                .FirstOrDefault(x => x.Id == id);
            return PartialView("_BookModalPartial", book);
        }

        public IActionResult AddToBasket(int id)
        {
            var basketStr = HttpContext.Request.Cookies["basket"];
            List<BasketCookieItemVM> cookieItems = null;

            if(basketStr == null)
            {
                cookieItems = new List<BasketCookieItemVM>();
            }
            else
            {
                cookieItems = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(basketStr);
            }

            BasketCookieItemVM cookieItem = cookieItems.FirstOrDefault(x=>x.BookId == id);
            if(cookieItem == null)
            {
                cookieItem = new BasketCookieItemVM
                {
                    BookId = id,
                    Count = 1
                };
                cookieItems.Add(cookieItem);
            }
            else
            {
                cookieItem.Count++;
            }

            HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(cookieItems));

            BasketVM basketVM = new BasketVM();
            foreach (var ci in cookieItems)
            {
                BasketItemVM item = new BasketItemVM
                {
                    Count= ci.Count,
                    Book = _context.Books.Include(x=>x.BookImages.Where(x=>x.POsterStatus==true)).FirstOrDefault(x=>x.Id==ci.BookId)
                };
                basketVM.Items.Add(item);
                basketVM.TotalAmount += (item.Book.DiscountPercent > 0 ? item.Book.SalePrice * (100 - item.Book.DiscountPercent) / 100 : item.Book.SalePrice) * item.Count;
            }

            return PartialView("_BasketPartial", basketVM);
        }

        public IActionResult ShowBasket()
        {
            var dataStr = HttpContext.Request.Cookies["basket"];
            var data = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(dataStr);
            return Json(data);
        }
    }
}
