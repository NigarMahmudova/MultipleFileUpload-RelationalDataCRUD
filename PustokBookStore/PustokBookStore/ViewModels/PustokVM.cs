using PustokBookStore.Entities;

namespace PustokBookStore.ViewModels
{
    public class PustokVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Feature> Features { get; set; }
        public List<Book> FeaturedBooks { get; set; }
        public List<Book> NewBooks { get; set; }
        public List<Book> DiscountedBooks { get; set; }
    }
}
