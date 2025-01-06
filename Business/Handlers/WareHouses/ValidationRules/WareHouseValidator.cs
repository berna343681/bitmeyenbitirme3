
using Business.Handlers.WareHouses.Commands;
using FluentValidation;

namespace Business.Handlers.WareHouses.ValidationRules
{

    public class CreateWareHouseValidator : AbstractValidator<CreateWareHouseCommand>
    {
        public CreateWareHouseValidator()
        {
            RuleFor(x => x.Category).NotEmpty();
            RuleFor(x => x.ProductName).NotEmpty();
            RuleFor(x => x.ColorName).NotEmpty();
            RuleFor(x => x.Size).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty();

        }
    }
    public class UpdateWareHouseValidator : AbstractValidator<UpdateWareHouseCommand>
    {
        public UpdateWareHouseValidator()
        {
            RuleFor(x => x.Category).NotEmpty();
            RuleFor(x => x.ProductName).NotEmpty();
            RuleFor(x => x.ColorName).NotEmpty();
            RuleFor(x => x.Size).NotEmpty();
            RuleFor(x => x.Quantity).NotEmpty();

        }
    }
}