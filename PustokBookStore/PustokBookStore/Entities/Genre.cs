using System.ComponentModel.DataAnnotations;

namespace PustokBookStore.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "20 simvoldan cox ola bilmez!")]
        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }
}
