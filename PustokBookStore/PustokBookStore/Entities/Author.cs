using System.ComponentModel.DataAnnotations;

namespace PustokBookStore.Entities
{
    public class Author
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }
        public List<Book> Books { get; set; }
    }
}
