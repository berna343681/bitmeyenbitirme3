
using Business.Handlers.ErrorRaports.Commands;
using FluentValidation;

namespace Business.Handlers.ErrorRaports.ValidationRules
{

    public class CreateErrorRaportValidator : AbstractValidator<CreateErrorRaportCommand>
    {
        public CreateErrorRaportValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Severity).NotEmpty();
            RuleFor(x => x.ComponentName).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.ErrorTypeName).NotEmpty();

        }
    }
    public class UpdateErrorRaportValidator : AbstractValidator<UpdateErrorRaportCommand>
    {
        public UpdateErrorRaportValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Severity).NotEmpty();
            RuleFor(x => x.ComponentName).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.ErrorTypeName).NotEmpty();

        }
    }
}