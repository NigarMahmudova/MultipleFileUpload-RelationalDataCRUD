using PustokBookStore.Attributes.CustomValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PustokBookStore.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Desc { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SalePrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal CostPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountPercent { get; set; }
        public bool StockStatus { get; set; }
        public bool IsNew { get; set;}
        public bool IsFeatured { get; set;}
        [NotMapped]
        [MaxFileLength(2097152)]
        [AllowedTypes("image/png, image/jpeg")]
        public IFormFile PosterFile { get; set; }
        [NotMapped]
        [MaxFileLength(2097152)]
        [AllowedTypes("image/png, image/jpeg")]
        public IFormFile HoverPosterFile { get; set; }
        [NotMapped]
        [MaxFileLength(2097152)]
        [AllowedTypes("image/png, image/jpeg")]
        public List<IFormFile> ImageFiles { get; set; }
        [NotMapped]
        public List<int> TagIds { get; set; } = new List<int>();
        public Author Author { get; set; }
        public Genre Genre { get; set; }
        public List<BookTag> BookTags { get; set; } = new List<BookTag>();
        public List<BookImage> BookImages { get; set; } = new List<BookImage>();

    }
}
