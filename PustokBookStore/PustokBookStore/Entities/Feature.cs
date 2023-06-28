using System.ComponentModel.DataAnnotations;

namespace PustokBookStore.Entities
{
    public class Feature
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string Title { get; set; }
        public string Desc { get; set; }
        [Required]
        [MaxLength(25)]
        public string Icon { get; set; }
    }
}
