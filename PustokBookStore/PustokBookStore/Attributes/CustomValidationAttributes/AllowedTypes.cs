using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PustokBookStore.Attributes.CustomValidationAttributes
{
    public class AllowedTypes:ValidationAttribute
    {
        private readonly string[] _types;
        public AllowedTypes(params string[] types)
        {
            _types = types;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            List<IFormFile> list = new List<IFormFile>();
            if (value is IFormFile file)
            {
                list.Add(file);
            }
            else if(value is List<IFormFile> files)
            {
                list.AddRange(files);
            }

            foreach (var item in list)
            {
                if (!_types.Contains(item.ContentType))
                {
                    return new ValidationResult($"ImageFile content type must be one of them: {string.Join(",", _types)}");
                }
            }

            return ValidationResult.Success;
        }
    }
}
