
using Business.Handlers.Orderİtems.Commands;
using FluentValidation;

namespace Business.Handlers.Orderİtems.ValidationRules
{

    public class CreateOrderİtemValidator : AbstractValidator<CreateOrderİtemCommand>
    {
        public CreateOrderİtemValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty();
            RuleFor(x => x.Color).NotEmpty();
            RuleFor(x => x.Size).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty();

        }
    }
    public class UpdateOrderİtemValidator : AbstractValidator<UpdateOrderİtemCommand>
    {
        public UpdateOrderİtemValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty();
            RuleFor(x => x.Color).NotEmpty();
            RuleFor(x => x.Size).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty();

        }
    }
}