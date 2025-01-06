
using Business.Handlers.FeedBacks.Commands;
using FluentValidation;

namespace Business.Handlers.FeedBacks.ValidationRules
{

    public class CreateFeedBackValidator : AbstractValidator<CreateFeedBackCommand>
    {
        public CreateFeedBackValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.FeedbackMessage).NotEmpty();

        }
    }
    public class UpdateFeedBackValidator : AbstractValidator<UpdateFeedBackCommand>
    {
        public UpdateFeedBackValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.FeedbackMessage).NotEmpty();

        }
    }
}