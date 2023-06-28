using System.ComponentModel.DataAnnotations;

namespace PustokBookStore.Attributes.CustomValidationAttributes
{
    public class MaxFileLength:ValidationAttribute
    {
        private readonly int _maxLength;
        public MaxFileLength(int maxLength)
        {
            _maxLength = maxLength;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value is IFormFile)
            {
                var file = (IFormFile)value;

                if(file.Length > _maxLength)
                {
                    return new ValidationResult($"FileLength must be less or equal than {_maxLength/1024/1024} MB");
                }
            }
            else if(value is List<IFormFile> list)
            {
                foreach (var file in list)
                {
                    if (file.Length > _maxLength)
                    {
                        return new ValidationResult($"FileLength must be less or equal than {_maxLength / 1024 / 1024} MB");
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}
