using System.ComponentModel.DataAnnotations;

namespace PustokBookStore.Entities
{
    public class BookImage
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        [Required]
        [MaxLength(100)]
        public string ImageName { get; set; }
        public bool? POsterStatus { get; set; }
        public Book Book { get; set; }
    }
}
