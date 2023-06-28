namespace PustokBookStore.Areas.Manage.ViewModels
{
    public class PaginatedList<T>
    {
        public PaginatedList(List<T> items, int pageIndex, int totalPages)
        {
            Items = items;
            TotalPages = totalPages;
            PageIndex = pageIndex;
        }   
        public List<T> Items { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public bool HasNext => PageIndex < TotalPages;
        public bool HasPrev => PageIndex >1;

        public static PaginatedList<T> Create(IQueryable<T> query, int pageIndex, int pageSize)
        {
            var items = query.Skip((pageIndex - 1)*pageSize).Take(pageSize).ToList();
            var totalPages = (int)Math.Ceiling(query.Count() / (double)pageSize);
            return new PaginatedList<T>(items, pageIndex, totalPages);
        }
    }
}
