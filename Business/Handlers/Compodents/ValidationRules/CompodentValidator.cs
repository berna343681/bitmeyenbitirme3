
using Business.Handlers.Compodents.Commands;
using FluentValidation;

namespace Business.Handlers.Compodents.ValidationRules
{

    public class CreateCompodentValidator : AbstractValidator<CreateCompodentCommand>
    {
        public CreateCompodentValidator()
        {
            RuleFor(x => x.ComponentName).NotEmpty();

        }
    }
    public class UpdateCompodentValidator : AbstractValidator<UpdateCompodentCommand>
    {
        public UpdateCompodentValidator()
        {
            RuleFor(x => x.ComponentName).NotEmpty();

        }
    }
}