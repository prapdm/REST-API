using FluentValidation;
using System;
using System.IO;
using System.Linq;

namespace ShopApi.Models.Validators
{
    public class CreateUpdateProductDtoValidator : AbstractValidator<CreateUpdateProductDto>
    {

        private readonly long _fileSizeLimit;
        private string[] permittedExtensions = { ".jpg", ".gif", ".png" };

        public CreateUpdateProductDtoValidator()
        {

            _fileSizeLimit = 2097152;

            RuleFor(x => x.file)
                .Custom((value, context) =>
                {
                    if (value is not null)
                    {
                        if (value.Length > _fileSizeLimit)
                            context.AddFailure("Image", $"File size limit is {_fileSizeLimit} KB");

                        var ext = Path.GetExtension(value.FileName).ToLowerInvariant();
                        if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
                            context.AddFailure("Image", $"You can only upload {String.Join(", ", permittedExtensions)} ");
                    }

                });
        }
                
    }
}
