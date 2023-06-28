namespace PustokBookStore.ViewModels
{
    public class BasketVM
    {
        public List<BasketItemVM> Items { get; set; } = new List<BasketItemVM>();
        public decimal TotalAmount { get; set; }    
    }
}
