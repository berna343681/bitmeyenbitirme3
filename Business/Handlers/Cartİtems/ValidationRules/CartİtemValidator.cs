
using Business.Handlers.Cartİtems.Commands;
using FluentValidation;

namespace Business.Handlers.Cartİtems.ValidationRules
{

    public class CreateCartİtemValidator : AbstractValidator<CreateCartİtemCommand>
    {
        public CreateCartİtemValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.ProductName).NotEmpty();
            RuleFor(x => x.Color).NotEmpty();
            RuleFor(x => x.Size).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty();

        }
    }
    public class UpdateCartİtemValidator : AbstractValidator<UpdateCartİtemCommand>
    {
        public UpdateCartİtemValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.ProductName).NotEmpty();
            RuleFor(x => x.Color).NotEmpty();
            RuleFor(x => x.Size).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty();

        }
    }
}