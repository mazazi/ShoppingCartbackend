using FluentValidation;
using Tatweer.Application.Commands.Products;

namespace Tatweer.Application.Validators.Products
{
    public class SaveProductCommandValidator : AbstractValidator<SaveProductCommand>
    {
        public SaveProductCommandValidator()
        {
            RuleFor(a => a.Name).NotEmpty().NotNull().WithMessage("Product Name is required");
            RuleFor(a => a.Price).NotEmpty().NotNull().WithMessage("Price is required");
            RuleFor(a => a.Qty).NotEmpty().NotNull().WithMessage("Quantity is required");
        }
    }
}
