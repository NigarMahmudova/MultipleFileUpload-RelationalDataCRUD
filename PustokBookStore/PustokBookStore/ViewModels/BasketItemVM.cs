using PustokBookStore.Entities;

namespace PustokBookStore.ViewModels
{
    public class BasketItemVM
    {
        public Book Book { get; set; }  
        public int Count { get; set; }
    }
}
