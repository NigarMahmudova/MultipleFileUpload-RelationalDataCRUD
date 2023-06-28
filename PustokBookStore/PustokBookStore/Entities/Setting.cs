using MessagePack;
using System.ComponentModel.DataAnnotations;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace PustokBookStore.Entities
{
    public class Setting
    {
        [Key] 
        public string Key { get; set; }
        [Required]
        [MaxLength(100)]
        public string Value { get; set; }
    }
}
