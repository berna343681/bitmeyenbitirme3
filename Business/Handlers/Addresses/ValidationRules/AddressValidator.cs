
using Business.Handlers.Addresses.Commands;
using FluentValidation;

namespace Business.Handlers.Addresses.ValidationRules
{

    public class CreateAddressValidator : AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressValidator()
        {
            RuleFor(x => x.Header).NotEmpty();
            RuleFor(x => x.Street).NotEmpty();
            RuleFor(x => x.Neighborhood).NotEmpty();
            RuleFor(x => x.Avenue).NotEmpty();
            RuleFor(x => x.StreetNumber).NotEmpty();
            RuleFor(x => x.ApartmentNumber).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.Country).NotEmpty();
            RuleFor(x => x.FullAddress).NotEmpty();

        }
    }
    public class UpdateAddressValidator : AbstractValidator<UpdateAddressCommand>
    {
        public UpdateAddressValidator()
        {
            RuleFor(x => x.Header).NotEmpty();
            RuleFor(x => x.Street).NotEmpty();
            RuleFor(x => x.Neighborhood).NotEmpty();
            RuleFor(x => x.Avenue).NotEmpty();
            RuleFor(x => x.StreetNumber).NotEmpty();
            RuleFor(x => x.ApartmentNumber).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.Country).NotEmpty();
            RuleFor(x => x.FullAddress).NotEmpty();

        }
    }
}