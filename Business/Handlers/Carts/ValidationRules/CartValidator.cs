
using Business.Handlers.Carts.Commands;
using FluentValidation;

namespace Business.Handlers.Carts.ValidationRules
{

    public class CreateCartValidator : AbstractValidator<CreateCartCommand>
    {
        public CreateCartValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();

        }
    }
    public class UpdateCartValidator : AbstractValidator<UpdateCartCommand>
    {
        public UpdateCartValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();

        }
    }
}