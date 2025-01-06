
using Business.Handlers.Products.Commands;
using FluentValidation;

namespace Business.Handlers.Products.ValidationRules
{

    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty();
            RuleFor(x => x.Category).NotEmpty();
            RuleFor(x => x.ColorName).NotEmpty();
            RuleFor(x => x.Size).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty();

        }
    }
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty();
            RuleFor(x => x.Category).NotEmpty();
            RuleFor(x => x.ColorName).NotEmpty();
            RuleFor(x => x.Size).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty();

        }
    }
}