
using Business.Handlers.PhoneNumbers.Commands;
using FluentValidation;

namespace Business.Handlers.PhoneNumbers.ValidationRules
{

    public class CreatePhoneNumberValidator : AbstractValidator<CreatePhoneNumberCommand>
    {
        public CreatePhoneNumberValidator()
        {
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.Number).NotEmpty();
            RuleFor(x => x.CustomerId).NotEmpty();

        }
    }
    public class UpdatePhoneNumberValidator : AbstractValidator<UpdatePhoneNumberCommand>
    {
        public UpdatePhoneNumberValidator()
        {
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.Number).NotEmpty();
            RuleFor(x => x.CustomerId).NotEmpty();

        }
    }
}