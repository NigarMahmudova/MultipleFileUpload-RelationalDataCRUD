using PustokBookStore.Attributes.CustomValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PustokBookStore.Entities
{
    public class Slider
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Title1 { get; set; }
        [MaxLength(50)]
        public string Title2 { get; set; }
        [MaxLength(150)]
        public string Desc { get; set; }
        [MaxLength(50)]
        public string BtnText { get; set; }
        [MaxLength(150)]
        public string BtnUrl { get; set; }
        [MaxLength(100)]
        public string ImageName { get; set; }
        public int Order { get; set; }
        [NotMapped]
        [MaxFileLength(2097152)]
        [AllowedTypes("image/jpeg","image/png")]
        public IFormFile ImageFile { get; set; }
    }
}
